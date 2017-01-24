using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EyeXFramework;
using System.Diagnostics;

namespace BeckerBoxForEyeGaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stack<Button> _previous;
        private bool _isLast;
        private bool _calledPre;

        private requestTimer _Timer = new requestTimer(2000);
        private object _lock = new object ();

        //EyeX
        //private EyeXHost _eyeXHost;

        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            Mouse.OverrideCursor = Cursors.None;

            _Timer._pressIT += pressedBtn;

            //_trigger.Start();

            // Initialize the EyeX Host
            /*            
                        _eyeXHost = new EyeXHost();
                        _eyeXHost.Start();

                        var stream = _eyeXHost.CreateGazePointDataStream(Tobii.EyeX.Framework.GazePointDataMode.LightlyFiltered);

                        stream.Next += (s, e) =>
                        {


                        };
              */
            //---------------------------------------------

            _tBox.TextChanged += (sender, e) => { _tBox.ScrollToEnd(); };

            //used to go back to the previous page
            _previous = new Stack<Button>();

            //after user selected the demanded letter (or symbol), then it goes to the "last page", which has the option of Home, Delete, Space and the demanded letter if user want to repeat
            //since the stack behaves differently, we use this field to keep track which page are we (either the last page or any others)
            _isLast = false;

            //as mentioned above, stack behaves differently. hence we use this one to decide how stack behaves
            //Stack behavir:
            //      - if is either last page or the Homepage, then it does not pop!!
            //      - any other pages, it pops
            _calledPre = false;
            initBeckerBox();
            this.MouseMove += cursorLocalization;
        }

        private void _btnClick(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                _Timer._client = sender;
                _Timer.Start();
            }
        }

        private void _btnPrevious_Enter(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                _Timer._client = sender;
                _Timer._pressIT -= pressedBtn;
                _Timer._pressIT += Previous;
                _Timer.Start();
            }
        }

        private void _btnPrevious_Leave(object sender, RoutedEventArgs e)
        {
            lock (_lock)
            {
                _Timer.Stop();
                _Timer._client = null;
                _Timer._pressIT -= Previous;
                _Timer._pressIT += pressedBtn;
            }
        }

        private void _btnMouseLeave(object sender, MouseEventArgs e)
        {
            lock (_lock)
            {
                _Timer.Stop();
                _Timer._client = null;
            }
        }
    }
}
