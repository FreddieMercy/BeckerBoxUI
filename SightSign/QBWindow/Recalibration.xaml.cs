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
using Tobii_Eris_Library;
using EyeXFramework;

namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for Recalibration.xaml
    /// </summary>
    public partial class Recalibration : Window
    {
        private newTimer _Timers = new newTimer(1000);
        private int time;
        private Window Owner;
        private List<ControlsXYandWidthHeight> tmpBtnCollection;
        private EyeXHost _eyeXHost;

        #region Buttons Init
        private List<IsControlXYandWidthHeight> _btn;
        #endregion

        public Recalibration(int _timeToWaitBeforeRecalibrate, List<ControlsXYandWidthHeight> TmpBtnCollection, Window Main, EyeXHost e)
        {
            InitializeComponent();

            _eyeXHost = e;
            Owner = Main;
            tmpBtnCollection = TmpBtnCollection;
            time = _timeToWaitBeforeRecalibrate;
            _Timers.Tick = RecalibrationStart;
            _CountDownTB.Text = time.ToString();

            //to make the ALL BUTTONS gaze-sensitive
            _btn = new List<IsControlXYandWidthHeight>();
            addAllBtns(_BBtn);
            addAllBtns(_CancelBtn);
            Owner.SizeChanged += manageThoseButtons;
        }

        //because when Qwerty and BB window resize, those buttons in Recalibration.xaml will be lost. this function adds thos buttons again
        private void manageThoseButtons(object sender, EventArgs e)
        {
            Owner.Dispatcher.BeginInvoke((Action)(() =>
            {
                foreach (IsControlXYandWidthHeight x in _btn)
                {
                    if (!tmpBtnCollection.Contains(x))
                    {
                        tmpBtnCollection.Add(x);
                    }
                }

            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void addAllBtns(Button btn)
        {
            IsControlXYandWidthHeight tmp = new IsControlXYandWidthHeight(btn);
            _btn.Add(tmp);
            tmpBtnCollection.Add(tmp);
        }

        private void remAllBtns()
        {
            foreach (IsControlXYandWidthHeight x in _btn)
            {
                if (tmpBtnCollection.Contains(x))
                {
                    tmpBtnCollection.Remove(x);
                }
            }
        }

        private void Button_Click(object sender = null, RoutedEventArgs e = null)
        {
            _Timers.Stop();

            this.Close();
        }

        private void RecalibrationStart(object sender, EventArgs e)
        {
            time--;
            _CountDownTB.Text = time.ToString();

            if (time == 0)
            {
                _eyeXHost.LaunchRecalibration();

                _Timers.Stop();
                /*
                Button recal = new Button() { Content = "Recalibrate", FontSize = 80, Margin = new Thickness(5) };
                recal.Click += RecalibrateClick;

                RecalibrateParent.Children.Remove(Recalibrate);
                RecalibrateParent.Children.Add(recal);

                addAllBtns(recal);
                */

                Button_Click();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Timers.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            remAllBtns();
            Owner.SizeChanged -= manageThoseButtons;
        }

        private void _BBtn_Click(object sender, RoutedEventArgs e)
        {
            _eyeXHost.LaunchRecalibration();
            Button_Click();
        }
    }
}
