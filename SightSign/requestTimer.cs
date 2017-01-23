using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using System.Windows.Controls;

namespace BeckerBox
{
	public class requestTimer
	{
		private Stopwatch _Stopwatch;
		public long _timeout { get; set; }
		private Thread _th;
		public object _client { get; set; }
        public event EventHandler _pressIT;

        public requestTimer (long timeout, object client = null)
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
                            _pressIT(_client, null);
                        }
                        this.Start();
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

        public void Reset(object sender)
        {
            this.Stop();
            _pressIT = null;
            _client = sender;
        }
	}
}

