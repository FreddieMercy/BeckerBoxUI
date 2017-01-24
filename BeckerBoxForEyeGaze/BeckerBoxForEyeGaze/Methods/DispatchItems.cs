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
        private void pressedBtn(object sender, EventArgs e = null)
        {
            Button btn = sender as Button;

            string[] tmp = new string[1];

            if (Dispatcher.CheckAccess())
            {
                pressBtnHelper(btn, tmp);
            }
            else
            {
                // if you don't want to block the current thread while action is
                // executed, you can also call Dispatcher.BeginInvoke(action);
                Dispatcher.Invoke(new Action(() => pressBtnHelper(btn, tmp)));
            }
        }

        private void pressBtnHelper(Button btn, string[] tmp)
        {
            if (btn.DataContext is string)
            {
                string s = btn.DataContext as string;
                tmp[0] = s;
            }
            else
            {
                try
                {
                    tmp = (string[])btn.DataContext;
                }
                catch (Exception)
                {
                    List<string> s = new List<string>();
                    s.Add(btn.DataContext as string);
                    tmp = s.ToArray();
                }
            }

            if (tmp.Length == 1)
            {
                keywordFuncParser(tmp[0], btn);
                return;
            }

            setName(btn);
        }


        //go back to the previous selection page, just in case if user chose the wrong one. 
        //it reads the previous all four btns contents that saved in the stack, however, since the stack is empty at the homepage, the stack pop or not dependends on the circumstands
        private void Previous(object sender = null, EventArgs e = null)
        {
            if (Dispatcher.CheckAccess())
            {
                PreviousHelper();
            }
            else
            {
                // if you don't want to block the current thread while action is
                // executed, you can also call Dispatcher.BeginInvoke(action);
                Dispatcher.Invoke(PreviousHelper);
            }
        }
        private void PreviousHelper()
        {
            _btnUpperLeft.IsEnabled = true;
            _btnUpperRight.IsEnabled = true;
            _btnBotLeft.IsEnabled = true;
            _btnBotRight.IsEnabled = true;

            if (!_isLast && !_calledPre)
            {
                _previous.Pop();
                _calledPre = true;
                _isLast = false;
            }

            if (_previous.Count > 0)
            {
                setName(_previous.Pop(), true);
            }

            if (_previous.Count == 0)
            {
                //Somehow the main menu's data context had been misplaced if we "Previous" back to it. Using Home() instead;
                /*
                _previous.Clear();
                _calledPre = false;

                initNames();

                _previous.Push(setupPrevious());
                */

                Home();
            }
        }
    }
}
