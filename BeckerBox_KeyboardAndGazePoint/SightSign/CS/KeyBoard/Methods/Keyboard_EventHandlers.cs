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
        private void Button_Click(object sender, EventArgs e)
        {
            _tBox.Text += (sender as Button).Content;
        }

        private void _Clear_Click(object sender, EventArgs e)
        {
            _tBox.Text = "";
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_tBox.Text) && _tBox.Text.Length > 0)
            {
                _tBox.Text = _tBox.Text.Substring(0, _tBox.Text.Length - 1);
            }
        }

        private void Enter_Button_Click(object sender, EventArgs e)
        {
            _tBox.Text += "\n";
        }
        private void Space_Button_Click(object sender, EventArgs e)
        {
            _tBox.Text += " ";
        }
    }

}