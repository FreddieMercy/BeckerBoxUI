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
    // This Window hosts two InkCanvases. The InkCanvas that's lower in the z-order shows ink 
    // which is to be traced out by an animating dot. As the dot moves, it leaves a trail of 
    // ink that's added to other InkCanvas. Also as the dot moves, the app moves a robot arm 
    // such that the arm follows the same path as the dot. 
    public partial class MainWindow : Window
    { 
        //for Becker Box, counting the gaze time
        private const long _timerInterval = 750;
        private requestTimer _Timer = new requestTimer(_timerInterval);
        //--------------------------------------

        private EyeXHost _eyeXHost = new EyeXHost();
        
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            Settings();

            //sort all the keys above to their locations as specified
            SortKeys();

            SetItemssourceOfKeyboardKeys();

            _Shift.Click += (sender, e) =>
            {
                SetItemssourceOfKeyboardKeys();
            };

            ((MainWindow)System.Windows.Application.Current.MainWindow).SizeChanged += setUpAllKeysStyle;
        }
        
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //GazeMouse.Attach(this); //DON'T DELETE IT!!!
            setUpAllKeysStyle(null, null);
        }

        //It is important to kill all other threads before completely exiting otherwise the program won't close properly
        //For whatever reason, c# destructors don't work so well so we have to explicitly tell the timer to kill its thread.
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {/*
            if (_eyeXHost.IsStarted)
            {
                GazeMouse.DetachAll(); //DON'T DELETE IT!!!
            }
            _Timer.KillThread();
            //base.OnExit(e);
            _eyeXHost.Dispose();
            // always dispose on exit
            */
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tBox.Text += (sender as Button).Content;
        }

        private void _Clear_Click(object sender, RoutedEventArgs e)
        {
            _tBox.Text = "";
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(_tBox.Text) && _tBox.Text.Length > 0)
            {
                _tBox.Text = _tBox.Text.Substring(0, _tBox.Text.Length - 1);
            }
        }

        private void Enter_Button_Click(object sender, RoutedEventArgs e)
        {
            _tBox.Text += "\n";
        }
        private void Space_Button_Click(object sender, RoutedEventArgs e)
        {
            _tBox.Text += " ";
        }

    }
    
}