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
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //for Becker Box, counting the gaze time

        private const long _timerInterval = 750;
        private requestTimer _Timer = new requestTimer(_timerInterval);

        //--------------------------------------

        private EyeXHost _eyeXHost = new EyeXHost();

        private void Settings()
        {
            SettingsForKeyboard();

        }
    }
}