using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BeckerBox
{
    public partial class MainWindow : Window
    {
        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                ClearTextQueue();
            }
            else
            {
                Dispatcher.Invoke(ClearTextQueue);
            }
        }

        private void DeleteCancelButton_Click(object sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                if (BoardType.MainBoard == mCurrentBoard && !EmptyTextQueue())
                    TextQueueTextBox.Text = TextQueueTextBox.Text.Substring(0, TextQueueTextBox.Text.Length - 1);
                else
                    ShowMainBoard();
            }
            else
            {
                Dispatcher.Invoke(()=> {
                    if (BoardType.MainBoard == mCurrentBoard && !EmptyTextQueue())
                        TextQueueTextBox.Text = TextQueueTextBox.Text.Substring(0, TextQueueTextBox.Text.Length - 1);
                    else
                        ShowMainBoard();
                });
            }
        }


        private void MainBoardGridClick(object sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                MainBoardGridClick_helper(sender, e);
            }
            else
            {
                // if you don't want to block the current thread while action is
                // executed, you can also call Dispatcher.BeginInvoke(action);
                Dispatcher.Invoke(new Action(() => MainBoardGridClick_helper(sender, e)));
            }
        }

        private void InnerBoardGridClick(object sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                InnerBoardGridClick_helper(sender, e);
            }
            else
            {
                // if you don't want to block the current thread while action is
                // executed, you can also call Dispatcher.BeginInvoke(action);
                Dispatcher.Invoke(new Action(() => InnerBoardGridClick_helper(sender, e)));
            }
        }
        private void InnerBoardGridClick_helper(object sender, EventArgs e)
        {
            TextBlock textBlockSender = sender as TextBlock;
            string inputLetter = "";
            if (ReferenceEquals(InnerLeftBox, textBlockSender))
                inputLetter = InnerLeftBox.Text;
            else if (ReferenceEquals(InnerUpperBox, textBlockSender))
                inputLetter = InnerUpperBox.Text;
            else if (ReferenceEquals(InnerRightBox, textBlockSender))
                inputLetter = InnerRightBox.Text;
            else if (ReferenceEquals(InnerBottomMiddleBox, textBlockSender))
                inputLetter = InnerBottomMiddleBox.Text;
            else if (ReferenceEquals(InnerBottomLeftBox, textBlockSender))
                inputLetter = InnerBottomLeftBox.Text;
            else if (ReferenceEquals(InnerBottomRightBox, textBlockSender))
                inputLetter = InnerBottomRightBox.Text;

            inputLetter = inputLetter.Replace(" ", "");
            TextQueueTextBox.Text += inputLetter;

            ShowMainBoard();
        }

        //Helper function to refresh screen

        private void MainBoardGridClick_helper(object sender, EventArgs e)
        {
            TextBlock textBlockSender = sender as TextBlock;
            lock (_lock)
            {
                if (null == _lockedSender)
                {
                    textBlockSender.Background = new SolidColorBrush(Colors.Green);
                    _lockedSender = textBlockSender;
                }
                else
                {
                    QueueLetter(_lockedSender);
                    _lockedSender.Background = new SolidColorBrush(Colors.White);
                    _lockedSender = null;
                }
            }
            /*
            if (ReferenceEquals(MainUpperLeftBox, textBlockSender))
            {
                InnerLeftBox.Text = "         A";
                InnerUpperBox.Text = "         B";
                InnerRightBox.Text = "         C";
                InnerBottomMiddleBox.Text = "         D";
                InnerBottomLeftBox.Text = "";
                InnerBottomRightBox.Text = "";
            }
            else if (ReferenceEquals(MainUpperMiddleBox, textBlockSender))
            {
                InnerLeftBox.Text = "         E";
                InnerUpperBox.Text = "         F";
                InnerRightBox.Text = "         G";
                InnerBottomMiddleBox.Text = "         H";
                InnerBottomLeftBox.Text = "";
                InnerBottomRightBox.Text = "";
            }
            else if (ReferenceEquals(MainUpperRightBox, textBlockSender))
            {
                InnerLeftBox.Text = "         I";
                InnerUpperBox.Text = "         J";
                InnerRightBox.Text = "         K";
                InnerBottomMiddleBox.Text = "         L";
                InnerBottomLeftBox.Text = "";
                InnerBottomRightBox.Text = "";
            }
            else if (ReferenceEquals(MainLowerLeftBox, textBlockSender))
            {
                InnerLeftBox.Text = "         M";
                InnerUpperBox.Text = "         N";
                InnerRightBox.Text = "         O";
                InnerBottomMiddleBox.Text = "         P";
                InnerBottomLeftBox.Text = "";
                InnerBottomRightBox.Text = "";
            }
            else if (ReferenceEquals(MainLowerMiddleBox, textBlockSender))
            {
                InnerLeftBox.Text = "         Q";
                InnerUpperBox.Text = "         R";
                InnerRightBox.Text = "         S";
                InnerBottomMiddleBox.Text = "         T";
                InnerBottomLeftBox.Text = "";
                InnerBottomRightBox.Text = "";
            }
            else if (ReferenceEquals(MainLowerRightBox, textBlockSender))
            {
                InnerLeftBox.Text = "         U";
                InnerUpperBox.Text = "         V";
                InnerRightBox.Text = "         W";
                InnerBottomMiddleBox.Text = "         X";
                InnerBottomLeftBox.Text = "         Y";
                InnerBottomRightBox.Text = "         Z";
            }
            
            ShowInnerBoard();*/

        }
    }
}
