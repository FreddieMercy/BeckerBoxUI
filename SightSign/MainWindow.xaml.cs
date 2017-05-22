using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Text;
using EyeXFramework;
using Tobii_Eris_Library;
using System.Windows.Media;
using System.Collections.Generic;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    { 
        public int SelectedIndex
        {
            set
            {
                _TabControl.SelectedIndex = value;
            }
        }

        public MainWindow(List<Button> _inputBtns, UserControl _Parent, StatisticsLogger s1, StatisticsLogger s2)
        {
            InitializeComponent();

            inputBtns = _inputBtns;
            myParent = _Parent;

            //Add the initial buttons in "BeckerBoxUI.xaml"
            foreach (Button x in inputBtns)
            {
                IsControlXYandWidthHeight tmp = new IsControlXYandWidthHeight(x);
                OwnerBtns.Add(tmp);
            }

            Settings(); //QWERTY

            //sort all the keys above to their locations as specified
            SortKeys(); //QWERTY

            SetItemssourceOfKeyboardKeys(); //QWERTY
            
            usd.PropertyChanged += keyboardDisplatingString; //QWERTY
            usd.PropertyChanged += bbDisplatingString; //BB
            
            usd.PropertyChanged += setUpTheSourceOfDisplayKey;  //QWERTY
                                                                //needs to be after "((MainWindow)System.Windows.Application.Current.MainWindow).SizeChanged += setUpAllKeysStyle;"
                                                                //needs to be before "CollectionAlltheButtonsInTheView();"

            usd.PropertyChanged += setUpAllKeysStyles_Loaded;  //QWERTY

            SizeChanged += setUpAllKeysStyles_Normal;   //QWERTY

            _TabControl.SelectionChanged += QWERTYSelected; //QWERTY
            _TabControl.SelectionChanged += BBSelected;     //BB
            _TabControl.SelectionChanged += CollectionMainBoxInTheView; //BB

            _TobiiFilterFor_eyeXHost = new GazingDataFilter(_pointsPerSecond, _eyeXHost);
            OutputGazeData(); //QWERTY AND BB
            setUpTheBBDot();  //BB
            addBoardBoxes();  //BB

            // Always "start" the logger (by instantiating it) as the last task in the MainWindow constructor
            m_sl = s1; // new StatisticsLogger("qwertyTestResults.txt", "QWERTY Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nQWERTY Keyboard Hover-To-Click time: " + _timerInterval + "ms"));
            m_s2 = s2; // new StatisticsLogger("bbTestResults.txt", "BB Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nBeckerBox Keyboard Hover-To-Click time: " + _timerInterval + "ms"));
        }

        private void OutputGazeData()
        {
            // Start the EyeX host.
            _eyeXHost.Start();
            
            var lightlyFilteredGazeDataStream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

            // This line below creates a new thread that will constantly run, read data, and put update _currentDataStream with the next piece of data that got read

            lightlyFilteredGazeDataStream.Next += (s, args) =>
            {
                //use "BeginInvoke", since the code below runs all the time and generates huge amount of data and easy to crash if shutdown suddenly
                Dispatcher.BeginInvoke((Action)(() =>
                { 
                    GazePointEventArgs e = _TobiiFilterFor_eyeXHost.TobiiCustomizedGazePointFilter(args);
                    TabItem BBinMain = myParent.Parent as TabItem;
                    TabControl BBMain = BBinMain.Parent as TabControl;
                    
                    if (BBMain.SelectedIndex != BBMain.Items.IndexOf(BBinMain))
                    {
                        takingInput = false;
                        WindowState = WindowState.Minimized;
                    }
                    
                    //Close both eyes for short period, then open both eyes to open the "BB" tab from the main interface
                    if (_eyeXHost.IsStarted && _eyeXHost.GazeTracking.Value != Tobii.EyeX.Framework.GazeTracking.GazeTracked)
                    {
                        if (WindowState != WindowState.Minimized)
                        {
                            takingInput = false;
                            WindowState = WindowState.Minimized;
                            foreach (Button x in inputBtns)
                            {
                                x.IsEnabled = true;
                            }
                        }
                        else
                        {
                            BBMain.SelectedIndex = BBMain.Items.IndexOf(BBinMain);
                            takingInput = true;
                            return;
                        }
                    }
                    if (takingInput)
                    {
                        mScreenCoordinates.X = e.X;
                        mScreenCoordinates.Y = e.Y;

                        if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(QWERTYKeyTab)) //Keyboard
                        {
                            #region
                            // *** keyboard ***

                            if (GazingOnObj != tmpBtnCollection.GetElements<Button>(e.X, e.Y))
                            {
                                if (GazingOnObj != null)
                                {
                                    (GazingOnObj as Control).Background = btnColor;

                                    GazingOnObj.Focusable = false;

                                    _Timer.Stop(); //Only place that "_Timer stops" for "QWERTY Keyboard", if anyone added others please mark
                                }

                                GazingOnObj = tmpBtnCollection.GetElements<Button>(e.X, e.Y);
                                inTobiiStreamFoundGazingElement(GazingOnObj);
                            }

                            #endregion
                        }
                        else if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(BeckerBoxTab)) //BeckerBox
                        {
                            #region

                            currentDot.Margin = new Thickness(e.X - dotSize / 2 - BeckerBoxUI.PointToScreen(new Point(0d, 0d)).X, e.Y - dotSize / 2 - BeckerBoxUI.PointToScreen(new Point(0d, 0d)).Y/*- System.Windows.Forms.SystemInformation.ToolWindowCaptionHeight*/, 0, 0); // Sets the position.

                            //Generating the Mainbox Events
                            if (GazingOnObj != MainBoxInTheView.GetElements<UIElement>(e.X, e.Y))
                            {
                                if (GazingOnObj != null)
                                {
                                    //Leaving
                                    _Timer.Stop();
                                    MouseLeaveBox(GazingOnObj, null);
                                }

                                //Entering
                                GazingOnObj = MainBoxInTheView.GetElements<UIElement>(e.X, e.Y);
                                inTobiiStreamFoundGazingElement(GazingOnObj);
                            }

                            //Generating the Innerbox Events
                            /* Woops, there is no need to generate innerbox events at this point */
                            #endregion
                        }
                    }                 

                }), System.Windows.Threading.DispatcherPriority.Loaded);
            };
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            /*
            GazeMouse.Attach(this); //DON'T DELETE IT!!! For the next year so they know there exists such thing
                                    //using Microsoft.HandsFree.Mouse;
           */
                       
            _tBox.Text = endingCode;
            btnColor = GetValueFromStyle(typeof(Button), BackgroundProperty) as Brush;
            WindowState = WindowState.Minimized; //Don't change it, since the "setUpAllKeysStyle" subscribe it, and "setUpAllKeysStyle" contains "CollectionAlltheButtonsInTheView"            
        }

        //It is important to kill all other threads before completely exiting otherwise the program won't close properly
        //For whatever reason, c# destructors don't work so well so we have to explicitly tell the timer to kill its thread.
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            /*
            Attention ---> if (_eyeXHost.IsStarted)
            {
                GazeMouse.DetachAll(); //DON'T DELETE IT!!!
            }
            
            */

            takingInput = false;
            _Timer.Stop();
            _eyeXHost.Dispose();
            _TobiiFilterFor_eyeXHost.Dispose();

            Keys.Clear();

            m_sl.AddComment("Number of Deletes pressed: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfDeletesPressed));
            m_sl.AddComment("Number of Spelling Errors: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfSpellingErrors));
            m_sl.AddComment("Number of Keys pressed pressed: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfKeysPressed));
            m_sl.AddNewLine();

            // always dispose on exit
        }
    }
    
}