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
using System.Windows.Shapes;

namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for BBUI.xaml
    /// </summary>
    public partial class BBUI : BeckerBoxUI
    {
        public BBUI()
        {
            InitializeComponent();
            usd.PropertyChanged += bbDisplatingString;
            SizeChanged += CollectionMainBoxInTheView;

            OutputGazeData();
            addBoardBoxes();
            // Always "start" the logger (by instantiating it) as the last task in the MainWindow constructor
            //m_s2 = new StatisticsLogger("bbTestResults.txt", "BB Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nBeckerBox Keyboard Hover-To-Click time: " + _timerInterval + "ms"));

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
                            Window_BBMouseMove(e.X, e.Y);

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
    }

}
