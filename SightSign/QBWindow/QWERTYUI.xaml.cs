using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Tobii_Eris_Library;
using EyeXFramework;
using System.ComponentModel;

namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for QWERTYUI.xaml
    /// </summary>
    public partial class QWERTYUI : BeckerBoxUI, INotifyPropertyChanged
    {
        public QWERTYUI()
        {
            InitializeComponent();
            Settings();

            //sort all the keys above to their locations as specified
            SortKeys();

            SetItemssourceOfKeyboardKeys();

            usd.PropertyChanged += keyboardDisplatingString;

            usd.PropertyChanged += setUpTheSourceOfDisplayKey; //needs to be after "((MainWindow)System.Windows.Application.Current.MainWindow).SizeChanged += setUpAllKeysStyle;"
                                                               //needs to be before "CollectionAlltheButtonsInTheView();"

            usd.PropertyChanged += setUpAllKeysStyles_Loaded;

            SizeChanged += setUpAllKeysStyles_Normal;

            OutputGazeData();
            // Always "start" the logger (by instantiating it) as the last task in the MainWindow constructor
            m_sl = new StatisticsLogger("qwertyTestResults.txt", "QWERTY Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nQWERTY Keyboard Hover-To-Click time: " + _timerInterval + "ms"));

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
                    lock (_lockInMains)
                    {
                        GazePointEventArgs e = _TobiiFilterFor_eyeXHost.TobiiCustomizedGazePointFilter(args);
                        if (takingInput)
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

                            //To Pat: Are you sure you still want it? 
                            /*
                            _currentStreamData = new PointXY(e.X, e.Y);
                            Windowk_MouseMove(null, null);
                            */


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
            //FindUtinityBtns();
            WindowState = WindowState.Maximized; //Don't change it, since the "setUpAllKeysStyle" subscribe it, and "setUpAllKeysStyle" contains "CollectionAlltheButtonsInTheView"
            _TobiiFilterFor_eyeXHost = new GazingDataFilter(_pointsPerSecond);
            _Timer = new requestTimers(_timerInterval);
        }

        //It is important to kill all other threads before completely exiting otherwise the program won't close properly
        //For whatever reason, c# destructors don't work so well so we have to explicitly tell the timer to kill its thread.
        private void MainWindow_OnClosing(object sender, EventArgs e)
        {
            /*
            Attention ---> if (_eyeXHost.IsStarted)
            {
                GazeMouse.DetachAll(); //DON'T DELETE IT!!!
            }
            
            */

            takingInput = false;
            _Timer.KillThread();
            _eyeXHost.Dispose();
            _TobiiFilterFor_eyeXHost.Dispose();

            m_sl.AddComment("Number of Deletes pressed: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfDeletesPressed));
            m_sl.AddComment("Number of Spelling Errors: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfSpellingErrors));
            m_sl.AddComment("Number of Keys pressed pressed: " + Convert.ToString(m_ca.CalibrationAnalyzerErrorStatistics.NumberOfKeysPressed));
            m_sl.AddNewLine();

            m_sl.WriteLoggerDetails();
            // always dispose on exit
        }
    }

}