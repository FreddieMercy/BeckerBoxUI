using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.HandsFree.Mouse;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using EyeXFramework;
using System.Collections.ObjectModel;
using System.Collections;

namespace BeckerBox
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    { 
        public MainWindow()
        {
            InitializeComponent();

            Settings();

            //sort all the keys above to their locations as specified
            SortKeys();

            SetItemssourceOfKeyboardKeys();

            _Shift.Click += (sender, e) =>
            {
                SetItemssourceOfKeyboardKeys();
            };

            ((MainWindow)System.Windows.Application.Current.MainWindow).SizeChanged += setUpAllKeysStyle;

            _eyeXHost.Start();

            var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

            stream.Next += (s, e) =>
            {
                Dispatcher.BeginInvoke((Action)(() =>
                    {
                        //can be improved to reduce the complexity, if the "GazingOnObj" hasn't changed.
                        lock (_lock)
                        {
                            if (GazingOnObj != tmpBtnCollection.GetElements<Button>(e.X, e.Y))
                            {
                                if (GazingOnObj != null)
                                {
                                    GazingOnObj.Background = btnColor;
                                    _Timer.Stop();
                                }

                                GazingOnObj = tmpBtnCollection.GetElements<Button>(e.X, e.Y);
                                inTobiiStreamFoundGazingElement(GazingOnObj);
                            }
                        }

                    }), System.Windows.Threading.DispatcherPriority.Normal);
            };

        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            /*
            GazeMouse.Attach(this); //DON'T DELETE IT!!! For the next year so they know there exists such thing
           */
            FindUtinityBtns();
            WindowState = WindowState.Maximized; //Don't change it, since the "setUpAllKeysStyle" subscribe it, and "setUpAllKeysStyle" contains "CollectionAlltheButtonsInTheView"

        }

        //It is important to kill all other threads before completely exiting otherwise the program won't close properly
        //For whatever reason, c# destructors don't work so well so we have to explicitly tell the timer to kill its thread.
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            /*
            Attention ---> if (_eyeXHost.IsStarted)
            {
                GazeMouse.DetachAll(); //DON'T DELETE IT!!!
            }
            
            */
            _Timer.KillThread();
            _eyeXHost.Dispose();
            // always dispose on exit
            
        }
    }
    
}