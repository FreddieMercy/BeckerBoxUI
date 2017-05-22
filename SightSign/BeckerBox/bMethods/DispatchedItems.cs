using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Forms;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void ClearButton_Click(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                ClearTextQueue();
            }), System.Windows.Threading.DispatcherPriority.Input);
        }

        private void DeleteCancelButton_Click(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                if (BoardType.MainBoard == mCurrentBoard && !EmptyTextQueue())
                    Delete_Button_Click();
                else
                    ShowMainBoard();
            }), System.Windows.Threading.DispatcherPriority.Input);

        }


        private void MainBoardGridClick(object sender, EventArgs e)
        {
            // if you don't want to block the current thread while action is
            // executed, you can also call Dispatcher.BeginInvoke(action);
            Dispatcher.BeginInvoke((Action)(() =>
            {
                MainBoardGridClick_helper(sender, e);
            }), System.Windows.Threading.DispatcherPriority.Input);

        }

        //Helper function to refresh screen

        private void MainBoardGridClick_helper(object sender, EventArgs e)
        {
            TextBlock textBlockSender = sender as TextBlock;
            lock (_lockz)
            {
                if(_eyeXHost.GazeTracking.Value != Tobii.EyeX.Framework.GazeTracking.GazeTracked)
                {
                    System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
                 
                    mScreenCoordinates.X = point.X;
                    mScreenCoordinates.Y = point.Y;
                }

                if (null == _lockedSender)
                {
                    textBlockSender.Background = new SolidColorBrush(Colors.Green);
                    _lockedSender = textBlockSender;
                }
                else
                {
                    QueueLetter(_lockedSender);
                    if (_lockedSender.IsMouseOver)
                        _lockedSender.Background = new SolidColorBrush(Colors.Aqua);
                    else
                        _lockedSender.Background = new SolidColorBrush(Colors.White);
                    _lockedSender = null;
                }
            }

        }
    }
}
