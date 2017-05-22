using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool EmptyTextQueue()
        {
            if (null == TextQueueTextBox.Text || 0 == TextQueueTextBox.Text.Length)
                return true;
            else
                return false;
        }

        private void ClearTextQueue()
        {
            usd.Clear();
        }

        private void QueueLetter(TextBlock toEval)
        {
            BoxSection boxSection = BoxSection.Up;
            MathHelper.Line topLeftToBottomRightLine = MathHelper.GetLineEquation(new MathHelper.Point2D(toEval.PointToScreen(new Point(0d, 0d)).X, toEval.PointToScreen(new Point(0d, 0d)).Y), new MathHelper.Point2D(toEval.PointToScreen(new Point(0d, 0d)).X + toEval.ActualWidth, toEval.PointToScreen(new Point(0d, 0d)).Y + toEval.ActualHeight));
            MathHelper.Line bottomLeftToTopRightLine = MathHelper.GetLineEquation(new MathHelper.Point2D(toEval.PointToScreen(new Point(0d, 0d)).X, toEval.PointToScreen(new Point(0d, 0d)).Y + toEval.ActualHeight), new MathHelper.Point2D(toEval.PointToScreen(new Point(0d, 0d)).X + toEval.ActualWidth, toEval.PointToScreen(new Point(0d, 0d)).Y));

            bool mouseBelowTopLeftToBottomRightLine = false, mouseBelowBottomLeftToTopRightLine = false;

            if (topLeftToBottomRightLine.EvalX(Convert.ToDouble(mScreenCoordinates.X)) >= mScreenCoordinates.Y)
                mouseBelowTopLeftToBottomRightLine = false;
            else
                mouseBelowTopLeftToBottomRightLine = true;

            if (bottomLeftToTopRightLine.EvalX(Convert.ToDouble(mScreenCoordinates.X)) >= mScreenCoordinates.Y)
                mouseBelowBottomLeftToTopRightLine = false;
            else
                mouseBelowBottomLeftToTopRightLine = true;

            if (mouseBelowTopLeftToBottomRightLine && mouseBelowBottomLeftToTopRightLine)
                boxSection = BoxSection.Down;
            else if (!mouseBelowTopLeftToBottomRightLine && mouseBelowBottomLeftToTopRightLine)
                boxSection = BoxSection.Right;
            else if (mouseBelowTopLeftToBottomRightLine && !mouseBelowBottomLeftToTopRightLine)
                boxSection = BoxSection.Left;
            else
                boxSection = BoxSection.Up;

            string inputLetter = "";

            if (toEval == ABCD_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "A";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "B";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "C";
                else
                    inputLetter = "D";
            }
            else if (toEval == EFGH_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "E";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "F";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "G";
                else
                    inputLetter = "H";
            }
            else if (toEval == IJLK_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "I";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "J";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "K";
                else
                    inputLetter = "L";
            }
            else if (toEval == OTHER_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "No";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "HELP!";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "Yes";
                else
                    inputLetter = "I don't know";
            }
            else if (toEval == MNOP_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "M";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "N";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "O";
                else
                    inputLetter = "P";
            }
            else if (toEval == QRST_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "Q";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "R";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "S";
                else
                    inputLetter = "T";
            }
            else if (toEval == UVWX_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "U";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "V";
                else if (boxSection == BoxSection.Right)
                    inputLetter = "W";
                else
                    inputLetter = "X";
            }
            else if (toEval == YZ_Box)
            {
                if (boxSection == BoxSection.Left)
                    inputLetter = "Y";
                else if (boxSection == BoxSection.Up)
                    inputLetter = "Z";
                else if (boxSection == BoxSection.Right)
                    inputLetter = " ";
                else
                    inputLetter = "'";
            }

            usd.Append(inputLetter);
            m_sl.LogData(inputLetter); //debug
        }

        private void ShowMainBoard()
        {
            DeleteCancelButton.Content = "Delete";
            foreach (TextBlock t in mMainBoardBoxes)
                t.Visibility = Visibility.Visible;
            foreach (Border b in mMainBorders)
                b.Visibility = Visibility.Visible;
            mCurrentBoard = BoardType.MainBoard;
        }

    }
}
