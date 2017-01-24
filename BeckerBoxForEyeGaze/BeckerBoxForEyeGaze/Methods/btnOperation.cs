using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeckerBoxForEyeGaze
{
    public partial class MainWindow : Window
    {
        //Delete one char from textblock.Text, cannot delete if textblock has no content
        private void Del()
        {
            if (_tBox.Text.Length > 0)
            {
                string s = _tBox.Text;
                _tBox.Text = s.Substring(0, s.Length - 1);
            }
        }

        //return directly to the start page by initializing the BeckerBox again.
        private void Home()
        {
            initBeckerBox();
        }

    }
}
