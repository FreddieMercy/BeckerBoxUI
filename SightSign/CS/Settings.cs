using System.ComponentModel;
using System.Windows;
using System.Text;
using Tobii_Eris_Library;
using EyeXFramework;
using System.Collections.Generic;
using System.Windows.Controls;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //For the "GazingDataFilter.TobiiCustomizedGazePointFilter(args)"
        private const int _pointsPerSecond = 5; //For instance, if "_pointsPerSecond = 10", that means "GazingDataFilter.TobiiCustomizedGazePointFilter(args)" returns "10 times per second"
                                               //Keep it's value as low as possible, since the requestTimer may not response that quickly
                                               //for Becker Box, counting the gaze time
        public static readonly long _timerInterval = 1000; //remember set the "_Timer._timeout = _timerInterval" every time when you use "_Timer", 
                                                  //since some Controls just need to wait longer than "_timerInterval"
        private newTimer _Timer = new newTimer(_timerInterval);

        List<Button> inputBtns;
        private List<ControlsXYandWidthHeight> OwnerBtns = new List<ControlsXYandWidthHeight> ();

        private UniversalDisplayString usd = new UniversalDisplayString(new StringBuilder(), _characterLimit);
        //--------------------------------------
        private UIElement GazingOnObj = null;
        /*
        private EyeXHost _eyeXHost = new EyeXHost();
        */
        public EyeXHost _eyeXHost = new EyeXHost();
        private GazingDataFilter _TobiiFilterFor_eyeXHost;

        private readonly int _timeToWaitBeforeRecalibrate = 5; //unit: second

        private bool takingInput = true;

        private UserControl myParent;

        private void Settings()
        {
            SettingsForKeyboard();
        }
    }
}