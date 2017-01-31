using EyeXFramework;
using Tobii.EyeX.Framework;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace BeckerBox
{
    public partial class App : Application
    {
        // Keep a reference to the host so it is not garbage 
        // collected.
        //private WpfEyeXHost _eyeXHost = new WpfEyeXHost();
        public App()
        {/*
            _eyeXHost.Start();

            var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);
            
            stream.Next += (s, e) =>
            {
                //SetCursorPos((int)e.X, (int)e.Y);
            };
            */
        }
        
        /*
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _eyeXHost.Dispose();
            // always dispose on exit
        }
        
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        */
    }
}