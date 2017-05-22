using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;


namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void MouseEnterBox(object sender, EventArgs e)
        {
            TextBlock textBlockSender = sender as TextBlock;
            textBlockSender.Background = new SolidColorBrush(Colors.Aqua);

            MouseEnterBtn(sender, e);
        }

        private void MouseLeaveBox(object sender, EventArgs e)
        {
            if (sender is TextBlock)
            {
                if (null == _lockedSender)
                {
                    TextBlock textBlockSender = sender as TextBlock;
                    textBlockSender.Background = new SolidColorBrush(Colors.White);

                    MouseLeaveBtn(sender, e);
                }
                else if (_lockedSender != sender)
                {
                    TextBlock textBlockSender = sender as TextBlock;
                    textBlockSender.Background = new SolidColorBrush(Colors.White);
                }
            }
            else if (sender is Button)
            {
                (sender as Control).Background = btnColor;
                (sender as Control).Focusable = false;
            }
        }

        private void MouseEnterBtn(object sender, EventArgs e)
        {
            lock (_lockz)
            {
                _Timer.Stop();
                //remember to add all the gaze-clickable items in here
                if (mMainBoardBoxes.Contains(sender))
                {
                    _Timer.Tick = (s, es) => { MainBoardGridClick(sender, e); };
                }
                else if (ReferenceEquals(sender, ClearButton))
                {
                    _Timer.Tick = (s, es) => { ClearButton_Click(sender, e); };
                }

                else if (ReferenceEquals(sender, DeleteCancelButton))
                {
                    _Timer.Tick = (s, es) => { DeleteCancelButton_Click(sender, e); };
                }

                _Timer.Start();
            }
        }

        private void MouseLeaveBtn(object sender, EventArgs e)
        {
            lock (_lockMouseLeaveBtns)
            {
                _Timer.Stop();
            }
        }
    }
}
