using EyeXFramework;
using System;
using System.Runtime.InteropServices;
using Tobii.EyeX.Framework;
using System.Windows;
using EyeXFramework.Wpf;

namespace BeckerBoxForEyeGaze
{
    public partial class App : Application
    {
        // Keep a reference to the host so it is not garbage 
        // collected.
        private WpfEyeXHost _eyeXHost;
        public App()
        {
            _eyeXHost = new WpfEyeXHost();

            _eyeXHost.Start();

            var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

            stream.Next += (s, e) =>
            {
                SetCursorPos((int)e.X, (int)e.Y);
            };
            
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _eyeXHost.Dispose();
            // always dispose on exit
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

    }
}
