using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using Microsoft.HandsFree.Mouse;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using EyeXFramework;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BeckerBox
{
	public class requestTimer
	{
		private Stopwatch _Stopwatch;
		public long _timeout { get; set; }
		private Thread _th;
		public UIElement _client { get; set; }
        public event EventHandler _pressIT;
        private object _lock = new object();

        public requestTimer (long timeout, UIElement client = null)
		{
			_Stopwatch = new Stopwatch ();
			_timeout = timeout;
			_client = client;
		    _th = new Thread (timing);
			_th.Start();
		}

        //c# destructors don't work well, so leave this alone
        ~requestTimer()
        {

        }

		public void Start()
		{
            if (_client != null)
            {
                _Stopwatch.Reset();
                _Stopwatch.Start();
            }
            /*
            else
            {
                throw new NullReferenceException("Selected Becker Box or Letter is NULL (unset)!!");
            }
            */

        }

        public void KillThread()
        {

            _th.Abort();
            _th = null;
        }

		private void timing()
		{
			while(true)
            {
                if (_Stopwatch.ElapsedMilliseconds >= _timeout)
                {

                    if (_client != null)
                    {

                        this.Stop();
                        var handler = _pressIT;
                        if (null != handler)
                        {
                            _client.Dispatcher.BeginInvoke((Action)(() =>
                            {
                                        //can be improved to reduce the complexity, if the "GazingOnObj" hasn't changed.
                                        lock (_lock)
                                {
                                    _pressIT(_client, null);
                                }
                            }), System.Windows.Threading.DispatcherPriority.Normal);
                            this.Start();
                        }


                    }
                    /*
                    else
                    {
                        throw new NullReferenceException("Selected Becker Box or Letter is NULL (unset)!!");
                    }
                    */
                }

			}
            
		}


		public void Stop()
		{
            if (_client != null)
            {
                _Stopwatch.Stop();
                //Console.WriteLine ("Timer stopped. Duration : " + _Stopwatch.ElapsedMilliseconds);
                _Stopwatch.Reset();
                //reset button
            }
            /*
            else
            {
                throw new NullReferenceException("Selected Becker Box or Letter is NULL (unset)!!");
            }
            */
        }

        public void Reset(UIElement sender)
        {
            this.Stop();
            _pressIT = null;
            _client = sender;
        }
	}
}

