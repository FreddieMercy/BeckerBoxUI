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
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using Tobii_Eris_Library;

namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for QWERTYUI.xaml
    /// </summary>
    public partial class QWERTYUI : BeckerBoxUI, INotifyPropertyChanged
    {
        private void keyboardDisplatingString(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != UniversalDisplayString.PropertyChanged_CountDown)
            {
                _tBox.Text = (sender as UniversalDisplayString).String + endingCode;
            }
        }

        private void _tmpKeyboardDisplatingSubString(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != UniversalDisplayString.PropertyChanged_CountDown)
            {
                _tBox.Text = _DisplayAndInsertKeyBoardTextBeginningString + (sender as UniversalDisplayString).String + endingCode;
            }
        }

        private void DisplayingClickHandler(object sender, RoutedEventArgs e)
        {
            _Minus_ButtonStyle_prepareForTheInsertIntoUsd(sender as Button);
            _Minus_EventHandlers_prepareForTheInsertIntoUsd();

            usd.String = (sender as Button).Content.ToString();
            
            _Plus_DoneButtonSetup();

            _tbBotLeft.IsEnabled = false;
        }

        private void DoneBtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (sender as Button).Tag as Button;
            _tbBotLeft.IsEnabled = true;
            _Minus_DoneButtonSetup();
            _Plus_ButtonStyle_prepareForTheInsertIntoUsd(btn);
            _Plus_EventHandlers_prepareForTheInsertIntoUsd();
            _Plus_UIElementsAndInfo_prepareForTheInsertIntoUsd(btn);
        }

        private void setUpTheSourceOfDisplayKey(object sender, EventArgs e)
        {
            _tbBotLeft.ItemsSource = usd.getCountDownItems();
        }
    }
}