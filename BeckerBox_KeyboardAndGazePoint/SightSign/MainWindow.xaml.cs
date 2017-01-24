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
using EyeXFramework.Wpf;
using Tobii.EyeX.Framework;

namespace BeckerBox
{
    // This Window hosts two InkCanvases. The InkCanvas that's lower in the z-order shows ink 
    // which is to be traced out by an animating dot. As the dot moves, it leaves a trail of 
    // ink that's added to other InkCanvas. Also as the dot moves, the app moves a robot arm 
    // such that the arm follows the same path as the dot. 
    public partial class MainWindow : Window
    {
        //for Becker Box, counting the gaze time
        private const long _timerInterval = 750;
        private requestTimer _Timer = new requestTimer(_timerInterval);
        private object _lock = new object();
        private TextBlock _lockedSender = null;
        //--------------------------------------
        private enum BoardType
        {
            MainBoard,
            InnerBoxBoard
        }

        private enum BoxSection
        {
            Up,
            Left,
            Right,
            Down
        }

        //All the becker boxes
        private BoardType mCurrentBoard;
        private List<TextBlock> mMainBoardBoxes;
        private List<TextBlock> mInnerBoardBoxes;
        //Borders
        private List<Border> mMainBorders;
        private List<Border> mInnerBorders;

        MathHelper.Point2D mScreenCoordinates;
        //private WpfEyeXHost _eyeXHost = new WpfEyeXHost();
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            /*
            _eyeXHost.Start();

            var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

            stream.Next += (s, e) =>
            {
                //SetCursorPos((int)e.X, (int)e.Y);
                MessageBox.Show("Nihaoma");
            };
            */
            //Hide cursor, since we are controlling the cursor
            //Mouse.OverrideCursor = Cursors.None; //temp

            mMainBoardBoxes = new List<TextBlock>();
            mMainBoardBoxes.Add(MainUpperLeftBox);
            mMainBoardBoxes.Add(MainUpperMiddleBox);
            mMainBoardBoxes.Add(MainUpperRightBox);
            mMainBoardBoxes.Add(MainLowerLeftBox);
            mMainBoardBoxes.Add(MainLowerMiddleBox);
            mMainBoardBoxes.Add(MainLowerRightBox);

            mInnerBoardBoxes = new List<TextBlock>();
            mInnerBoardBoxes.Add(InnerUpperBox);
            mInnerBoardBoxes.Add(InnerLeftBox);
            mInnerBoardBoxes.Add(InnerRightBox);
            mInnerBoardBoxes.Add(InnerBottomLeftBox);
            mInnerBoardBoxes.Add(InnerBottomMiddleBox);
            mInnerBoardBoxes.Add(InnerBottomRightBox);

            mMainBorders = new List<Border>();
            mMainBorders.Add(MainUpperLeftBorder);
            mMainBorders.Add(MainUpperMiddleBorder);
            mMainBorders.Add(MainUpperRightBorder);
            mMainBorders.Add(MainLowerLeftBorder);
            mMainBorders.Add(MainLowerMiddleBorder);
            mMainBorders.Add(MainLowerRightBorder);

            mInnerBorders = new List<Border>();
            mInnerBorders.Add(InnerUpperBorder);
            mInnerBorders.Add(InnerLeftBorder);
            mInnerBorders.Add(InnerRightBorder);
            mInnerBorders.Add(InnerBottomLeftBorder);
            mInnerBorders.Add(InnerBottomMiddleBorder);
            mInnerBorders.Add(InnerBottomRightBorder);

            mScreenCoordinates = new MathHelper.Point2D();

            ShowMainBoard();
        }

        //c# destructors don't work well, so leave this alone
        ~MainWindow()
        {

        }

        private bool EmptyTextQueue()
        {
            if (null == TextQueueTextBox.Text || 0 == TextQueueTextBox.Text.Length)
                return true;
            else
                return false;
        }

        private void ClearTextQueue()
        {
            TextQueueTextBox.Text = "";
        }

        private void QueueLetter(TextBlock toEval)
        {
            BoxSection boxSection = BoxSection.Up;
            MathHelper.Line topLeftToBottomRightLine = MathHelper.GetLineEquation(new MathHelper.Point2D(toEval.Margin.Left, toEval.Margin.Top), new MathHelper.Point2D(toEval.Margin.Left + toEval.Width, toEval.Margin.Top + toEval.Height));
            MathHelper.Line bottomLeftToTopRightLine = MathHelper.GetLineEquation(new MathHelper.Point2D(toEval.Margin.Left, toEval.Margin.Top + toEval.Height), new MathHelper.Point2D(toEval.Margin.Left + toEval.Width, toEval.Margin.Top));

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

            if (toEval == MainUpperLeftBox)
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
            else if (toEval == MainUpperMiddleBox)
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
            else if (toEval == MainUpperRightBox)
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
            else if (toEval == MainLowerLeftBox)
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
            else if (toEval == MainLowerMiddleBox)
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
            else //Lower Right Box
            {
                //Y X Z
                if (mScreenCoordinates.Y >= 620)
                {
                    //Y
                    if (mScreenCoordinates.X <= 1236)
                        inputLetter = "Y";
                    //Z
                    else if (mScreenCoordinates.X >= 1414)
                        inputLetter = "Z";
                    //X
                    else
                        inputLetter = "X";
                }
                //U V W
                else
                {
                    //U
                    if (mScreenCoordinates.X <= 1236)
                        inputLetter = "U";
                    //W
                    else if (mScreenCoordinates.X >= 1414)
                        inputLetter = "W";
                    //V
                    else
                        inputLetter = "V";
                }
            }

            TextQueueTextBox.Text += inputLetter;
        }

        private void ShowMainBoard()
        {
            HideInnerBoard();
            DeleteCancelButton.Content = "Delete";
            foreach (TextBlock t in mMainBoardBoxes)
                t.Visibility = Visibility.Visible;
            foreach (Border b in mMainBorders)
                b.Visibility = Visibility.Visible;
            mCurrentBoard = BoardType.MainBoard;
        }

        /*
        private void HideMainBoard()
        {
            foreach (TextBlock t in mMainBoardBoxes)
                t.Visibility = Visibility.Hidden;
            foreach (Border b in mMainBorders)
                b.Visibility = Visibility.Hidden;
        }

        
        private void ShowInnerBoard()
        {
            HideMainBoard();
            DeleteCancelButton.Content = "Cancel";
            foreach (TextBlock t in mInnerBoardBoxes)
                t.Visibility = Visibility.Visible;
            foreach (Border b in mInnerBorders)
                b.Visibility = Visibility.Visible;
            mCurrentBoard = BoardType.InnerBoxBoard;
        }
        */

        private void HideInnerBoard()
        {
            foreach (TextBlock t in mInnerBoardBoxes)
                t.Visibility = Visibility.Hidden;
            foreach (Border b in mInnerBorders)
                b.Visibility = Visibility.Hidden;
        }

        private void MouseEnterBox(object sender, EventArgs e)
        {
            TextBlock textBlockSender = sender as TextBlock;
            textBlockSender.Background = new SolidColorBrush(Colors.Aqua);

            MouseEnterBtn(sender, e);
        }

        private void MouseLeaveBox(object sender, EventArgs e)
        {
            if (null == _lockedSender)
            {
                TextBlock textBlockSender = sender as TextBlock;
                textBlockSender.Background = new SolidColorBrush(Colors.White);

                MouseLeaveBtn(sender, e);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            mScreenCoordinates.X = e.GetPosition(null).X;
            mScreenCoordinates.Y = e.GetPosition(null).Y;
            ScreenCoordinatesTextBox.Text = e.GetPosition(null).X + ", " + e.GetPosition(null).Y;
        }

        private void MouseEnterBtn(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _Timer.Reset(sender);

                //remember to add all the gaze-clickable items in here
                if (mMainBoardBoxes.Contains(sender))
                {
                    _Timer._pressIT += MainBoardGridClick;
                }
                else if (mInnerBoardBoxes.Contains(sender))
                {
                    _Timer._pressIT += InnerBoardGridClick;
                }
                else if (ReferenceEquals(sender, ClearButton))
                {
                    _Timer._pressIT += ClearButton_Click;
                }

                else if (ReferenceEquals(sender, DeleteCancelButton))
                {
                    _Timer._pressIT += DeleteCancelButton_Click;
                }

                _Timer.Start();
            }
        }

        private void MouseLeaveBtn(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _Timer.Stop();
                _Timer._client = null;
            }
        }

        //DON'T DELETE THIS METHOD!!!
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            GazeMouse.Attach(this); //DON'T DELETE IT!!!
        }

        //It is important to kill all other threads before completely exiting otherwise the program won't close properly
        //For whatever reason, c# destructors don't work so well so we have to explicitly tell the timer to kill its thread.
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            GazeMouse.DetachAll(); //DON'T DELETE IT!!!
            _Timer.KillThread();
            //base.OnExit(e);
            //_eyeXHost.Dispose();
            // always dispose on exit
        }

    }
    
}