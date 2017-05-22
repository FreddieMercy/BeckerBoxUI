using System;
using System.Collections.Generic;
using System.Threading;
using EyeXFramework;
using System.Windows;

namespace Tobii_Eris_Library
{
    public class GazingDataFilter : IDisposable
    {
        #region Fields
        //_pointsPerSecond = 10
        private GazePointEventArgs Primary;
        private GazePointEventArgs Secondary;
        private GazePointEventArgs Result;

        private Thread _takenTheEArgs;
        private requestTimers _CalculateThePoint;

        private Stack<GazePointEventArgs> _stack;
        private object _lock = new object();

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _takenTheEArgs.Abort();
                    _CalculateThePoint.Stop();
                    _CalculateThePoint.KillThread();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        public GazingDataFilter(int _pointsPerSecond, EyeXHost eyeX)
        {
            Primary = null;
            Secondary = null;
            _stack = new Stack<GazePointEventArgs>();
            _takenTheEArgs = new Thread(inputSecondaryToDataStructure);
            _CalculateThePoint = new requestTimers(1000 / _pointsPerSecond, eyeX);
            _CalculateThePoint._client = new UIElement();
            _CalculateThePoint._pressIT += outputResultToPrimary;
        }

        public GazePointEventArgs TobiiCustomizedGazePointFilter(GazePointEventArgs e)
        {
            if (!disposedValue)
            {
                if (Primary == null)
                {
                    Primary = e;
                    Result = e;
                    _takenTheEArgs.Start();
                    _CalculateThePoint.Start();
                }
                else
                {
                    Secondary = e;
                    if (Result != null)
                    {
                        Primary = Result;
                    }
                }

                return Primary;
            }

            return e;
        }

        private void inputSecondaryToDataStructure()
        {
            //input Secondary
            while (true)
            {
                if (Secondary != null)
                {
                    _stack.Push(Secondary);
                    Secondary = null;
                }
            }
        }

        private void outputResultToPrimary(object sender, EventArgs e)
        {
            //output Result
            lock (_lock)
            {
                Result = _stack.TobiiFilterStackGetPoint();
                _stack.Clear();
            }
        }

    }

    public static class TobiiFilterStackExtensions
    {
        public static GazePointEventArgs TobiiFilterStackGetPoint(this Stack<GazePointEventArgs> list)
        {
            if (list.Count <= 0)
            {
                return null;
            }
            //Better query or thread to replace the following:
            double row = 0.0, col = 0.0;

            foreach (GazePointEventArgs x in list)
            {
                row += x.X;
                col += x.Y;
            }

            //or, if list.Count is huge, use threads; if list.Count is small, use query

            return new GazePointEventArgs(row / list.Count, col / list.Count, list.Peek().Timestamp);
        }
    }
}