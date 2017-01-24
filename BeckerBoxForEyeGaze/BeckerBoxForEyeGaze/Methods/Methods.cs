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
        //this file contains all the helper functions, that will be used in the methods in "btnOperation.cs"
 
        //it reads the content of the btn, and run the conreponsing methods of the selected btn's content.
        //it could be extended in the future
        private void keywordFuncParser(string sth, Button btn)
        {
            switch (sth)
            {
                case "Space":
                    _tBox.Text += " ";
                    //SpaceDelHomeRepeat(btn);
                    break;
                case "Delete":
                    Del();
                    break;
                case "Home":
                    Home();
                    break;
                default:
                    _tBox.Text += sth;
                    SpaceDelHomeRepeat(btn);
                    break;
            }
        }

        //last page functionality
        private void SpaceDelHomeRepeat(Button btn)
        {
            _isLast = true;

            _btnUpperLeft.IsEnabled = true;
            _btnUpperRight.IsEnabled = true;
            _btnBotLeft.IsEnabled = true;
            _btnBotRight.IsEnabled = true;

            string[] space = { "Space" };
            string[] del = { "Delete" };
            string[] home = { "Home" };

            if(_btnUpperLeft == btn)
            {
                _btnUpperRight.Content = space[0];
                _btnUpperRight.DataContext = space;
                _btnBotLeft.Content = del[0];
                _btnBotLeft.DataContext = del;
                _btnBotRight.Content = home[0];
                _btnBotRight.DataContext = home;
            }
            else if(_btnUpperRight==btn)
            {
                _btnUpperLeft.Content = space[0];
                _btnUpperLeft.DataContext = space;
                _btnBotLeft.Content = del[0];
                _btnBotLeft.DataContext = del;
                _btnBotRight.Content = home[0];
                _btnBotRight.DataContext = home;
            }
            else if (_btnBotLeft == btn)
            {
                _btnUpperLeft.Content = space[0];
                _btnUpperLeft.DataContext = space;
                _btnUpperRight.Content = del[0];
                _btnUpperRight.DataContext = del;
                _btnBotRight.Content = home[0];
                _btnBotRight.DataContext = home;
            }
            else
            {
                _btnUpperLeft.Content = space[0];
                _btnUpperLeft.DataContext = space;
                _btnUpperRight.Content = del[0];
                _btnUpperRight.DataContext = del;
                _btnBotLeft.Content = home[0];
                _btnBotLeft.DataContext = home;
            }
        }


        //sort all the content in the initializing arrays in the file "Settings.cs"
        //sort them so the content of the same btn can always be consist.
        private string[] universialSort(object DC)
        {
            //group string based on their category. Purpose of it is to always keep the string same category together in certain sequence.
            //Final layout should be: num, Cap, alph, symbols

            List<string> num = new List<string>();
            List<string> sym = new List<string>();
            List<string> alph = new List<string>();
            List<string> cap = new List<string>();

            string[] input;

            try
            {
                input = (string[])DC;
            }
            catch (Exception)
            {
                List<string> s = new List<string>();
                s.Add(DC as string);
                input  = s.ToArray();
            }

            foreach (string x in input)
            {
                int y = (int)Encoding.ASCII.GetBytes(x)[0];

                //0-9
                if (48 <= y && y <= 57)
                {
                    if (!num.Contains(x))
                    {
                        num.Add(x);
                    }
                }

                //Cap
                else if (65 <= y && y <= 90)
                {
                    if (!cap.Contains(x))
                    {
                        cap.Add(x);
                    }
                }

                //alph

                else if (97 <= y && y <= 122)
                {
                    if (!alph.Contains(x))
                    {
                        alph.Add(x);
                    }
                }

                else
                {
                    if (!sym.Contains(x))
                    {
                        sym.Add(x);
                    }
                }
            }

            //concat

            string[] nums = num.ToArray();
            string[] caps = cap.ToArray();
            string[] alphs = alph.ToArray();
            string[] syms = sym.ToArray();

            Array.Sort(nums, StringComparer.Ordinal);
            Array.Sort(caps, StringComparer.Ordinal);
            Array.Sort(alphs, StringComparer.Ordinal);
            Array.Sort(syms, StringComparer.Ordinal);

            int array1OriginalLength = nums.Length;

            Array.Resize<string>(ref nums, array1OriginalLength + caps.Length);
            Array.Copy(caps, 0, nums, array1OriginalLength, caps.Length);
            array1OriginalLength += caps.Length;

            Array.Resize<string>(ref nums, array1OriginalLength + alphs.Length);
            Array.Copy(alphs, 0, nums, array1OriginalLength, alphs.Length);
            array1OriginalLength += alphs.Length;

            Array.Resize<string>(ref nums, array1OriginalLength + syms.Length);
            Array.Copy(syms, 0, nums, array1OriginalLength, syms.Length);
            
            return nums;
        }

    }
}
