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
        //this file contains the methods to initialize the content of each btn. 
        //the methods produces almost all the contents of all the btns in all circumstands.
        private void initBeckerBox()
        {
            _previous.Clear();
            _calledPre = false;

            initNames();

            _previous.Push(setupPrevious());

            _btnUpperLeft.DataContext = btnUpperLeft;
            _btnUpperRight.DataContext = btnUpperRight;
            _btnBotLeft.DataContext = btnBotLeft;
            _btnBotRight.DataContext = btnBotRight;
            
        }

        private void initNames()
        {
            _btnUpperLeft.Content = btnUpperLeftName;
            _btnUpperRight.Content = btnUpperRightName;
            _btnBotLeft.Content = btnBotLeftName;
            _btnBotRight.Content = btnBotRightName;
        }


        //in fact, it sets the contents of each btn after each time user selected 
        private void setName(Button btn, bool pre = false)
        {
            if (!pre)
            {
                _previous.Push(new Button() { DataContext = btn.DataContext });
            }

            setArrayOfChosen(btn);

            _btnUpperLeft.Content = getNameFromArray(_btnUpperLeft.DataContext);
            _btnUpperRight.Content = getNameFromArray(_btnUpperRight.DataContext);
            _btnBotLeft.Content = getNameFromArray(_btnBotLeft.DataContext);
            _btnBotRight.Content = getNameFromArray(_btnBotRight.DataContext);
        }


        //all the names of each btn was were generated at here
        //basically, the algrithm of generating the name of each btn is: from content_array[0] to content_array[last]
        private string getNameFromArray(object DC)
        {
            string[] input = new string[1];

            if (DC is string)
            {

                string s = DC as string;
                input[0] = s;
            }
            else
            {
                try
                {
                    input = (string[])DC;
                }
                catch (Exception)
                {
                    List<string> s = new List<string>();
                    s.Add(DC as string);
                    input = s.ToArray();
                }
            }

            if (input == null || input.Length == 0)
            {
                return "";
            }

            if (input.Length > 1)
            {
                return "From " + input[0] + " to " + input[input.Length - 1] + "...";
            }

            return input[0];
        }

        private string[] subArray(int from, int to, string[] input)
        {
            if (input == null)
            {
                return null;
            }

            List<string> tmp = new List<string>();

            for (int i = from; i < to; i++)
            {
                tmp.Add(input[i]);
            }

            return tmp.ToArray();
        }


        //after each selection, the content of the chosen btn will be splited avergly (almost, unless content_array.Length % 4 != 0) and assign each pieces of the content array to each button
        //as their new contents
        private void setArrayOfChosen(Button btn)
        {
            string[] tmp = universialSort(btn.DataContext);

            if (tmp == null)
            {
                throw new ArgumentNullException("Null Array btn should not be selected! Check the code!");
            }

            if (tmp.Length < 4)
            {
                _btnUpperLeft.DataContext = null;
                _btnUpperRight.DataContext = null;
                _btnBotLeft.DataContext = null;
                _btnBotRight.DataContext = null;

                _btnUpperLeft.IsEnabled = false;
                _btnUpperRight.IsEnabled = false;
                _btnBotLeft.IsEnabled = false;
                _btnBotRight.IsEnabled = false;

                if (tmp.Length > 0)
                {
                    _btnUpperLeft.DataContext = tmp[0];
                    _btnUpperLeft.IsEnabled = true;
                }
                if (tmp.Length > 1)
                {
                    _btnUpperRight.DataContext = tmp[1];
                    _btnUpperRight.IsEnabled = true;
                }
                if (tmp.Length > 2)
                {
                    _btnBotLeft.DataContext = tmp[2];
                    _btnBotLeft.IsEnabled = true;
                }

                return;
            }

            switch (tmp.Length % 4)
            {
                default:

                    _btnUpperLeft.DataContext = subArray(0, tmp.Length / 4, tmp);
                    _btnUpperRight.DataContext = subArray(tmp.Length / 4, tmp.Length / 2, tmp);
                    _btnBotLeft.DataContext = subArray(tmp.Length / 2, 3 * (tmp.Length / 4), tmp);
                    _btnBotRight.DataContext = subArray(3 * (tmp.Length / 4), tmp.Length, tmp);

                    break;

                case 2:
                    _btnUpperLeft.DataContext = subArray(0, tmp.Length / 4, tmp);
                    _btnUpperRight.DataContext = subArray(tmp.Length / 4, tmp.Length / 2, tmp);
                    _btnBotLeft.DataContext = subArray(tmp.Length / 2, 3 * (tmp.Length / 4) + 1, tmp);
                    _btnBotRight.DataContext = subArray(3 * (tmp.Length / 4) + 1, tmp.Length, tmp);
                    break;
                case 3:
                    _btnUpperLeft.DataContext = subArray(0, tmp.Length / 4, tmp);
                    _btnUpperRight.DataContext = subArray(tmp.Length / 4, (tmp.Length / 2) + 1, tmp);
                    _btnBotLeft.DataContext = subArray((tmp.Length / 2) + 1, 3 * (tmp.Length / 4) + 1, tmp);
                    _btnBotRight.DataContext = subArray(3 * (tmp.Length / 4) + 1, tmp.Length, tmp);
                    break;
            }
        }

    }
}
