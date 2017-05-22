using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows;
using System.Collections.Generic;
using EyeXFramework;

namespace Tobii_Eris_Library
{
    /*
     Not thread-safe!!!
     i.e: When too many Data/Operation/Changes in short time, it will press the wrong btn.
     solve it please.
     */

    public class requestTimers //The reason why don't use System.Windows.Threading.DispatcherTimer() is: 1. we need to "Start/Stop" when the "_client != null" ONLY!!! 2. we need ms not s
    {
        private Stopwatch _Stopwatch;
        private Thread _th;
        public event EventHandler _pressIT;
        public long _timeout { get; private set; }
        public UIElement _client { get; set; }
        private EyeXHost _eyeX;

        public requestTimers(long timeout, EyeXHost eyeX, UIElement client = null)
        {
            _eyeX = eyeX;
            _Stopwatch = new Stopwatch();
            _timeout = timeout;
            _client = client;
            _th = new Thread(timing);
            _th.Start();
        }

        //c# destructors don't work well, so leave this alone
        ~requestTimers()
        {

        }

        public void Start()
        {
            if (_client != null && null != _pressIT)
            {
                _Stopwatch.Stop();
                _Stopwatch.Reset();
                _Stopwatch.Start();
            }
        }

        public void KillThread()
        {
            if (_th != null)
            {
                _th.Abort();
                _th = null;
            }

            _client = null;            
        }

        private void timing()
        {
            while (true)
            {
                #region

                if (_Stopwatch.ElapsedMilliseconds >= _timeout)
                {
                    _Stopwatch.Stop();
                    _Stopwatch.Reset();

                    if (_client != null && null != _pressIT)
                    {
                        _client.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            //can be improved to reduce the complexity, if the "GazingOnObj" hasn't changed.

                            if ((Window)System.Windows.Application.Current.MainWindow != null && _eyeX.IsStarted && 
                            _eyeX.GazeTracking.Value == Tobii.EyeX.Framework.GazeTracking.GazeTracked)
                            //Don't need this part of code, since it had been only used in "GazingDataFilter", and "GazingDataFilter" won't be active until a valid "GazePointEventArgs e" exists,
                            //and "GazePointEventArgs e" won't exist if _eyeXHost is not functional
                            {
                                _pressIT(_client, null);
                            }

                        }), System.Windows.Threading.DispatcherPriority.Send); //for the case of: when tobii is power off in the middle of running

                    }
                    _Stopwatch.Start();
                }

                #endregion
            }

        }

        public void Stop()
        {
            _Stopwatch.Stop();
            _Stopwatch.Reset();
        }

        public void Reset(UIElement sender = null)
        {
            _Stopwatch.Stop();
            _Stopwatch.Reset();
            _pressIT = null;
            _client = sender;
        }

    }


    public class newTimer : DispatcherTimer
    {
        private List<EventHandler> delegates = new List<EventHandler>();

        public newTimer(long timeout, UIElement client = null)
        {
            base.Tag = client;
            if (timeout > 1000)
            {
                base.Interval = new TimeSpan(0, 0, (int)(timeout / 1000));
            }
            else
            {
                base.Interval = new TimeSpan(0, 0, 1); //1 sec minimum
            }
        }
        
        public new EventHandler Tick
        {
            set
            {
                foreach (EventHandler eh in delegates)
                {
                    base.Tick -= eh;
                }
                base.Tick += value;
                delegates.Add(value);
            }
        }
    }
}
