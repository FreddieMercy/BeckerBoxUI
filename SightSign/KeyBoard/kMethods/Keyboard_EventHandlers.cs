using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void Button_Click(object sender=null, EventArgs e=null)
        {
            usd.Append((sender as Button).Content);
            m_ca.UpdateAnalyzer(usd.String, ref m_predictedWords);

            // For testing
            m_sl.LogData((sender as Button).Content as string);
            m_sl.LogData("Calibration Rating: " + m_ca.Rating);
            m_sl.AddNewLine();
        }

        private void _Clear_Click(object sender=null, EventArgs e=null)
        {
            usd.Clear();
            m_ca.UpdateAnalyzer(usd.String, ref m_predictedWords);
            m_ca.OnClear();

            // For testing
            m_sl.LogData("(clear)");
            m_sl.LogData("Calibration Rating: " + m_ca.Rating);
            m_sl.AddNewLine();
        }

        private void Delete_Button_Click(object sender=null, EventArgs e=null)
        {
            if (usd.Length > 0)
            {
                usd.Remove(usd.Length - 1, 1);
            }
            m_ca.OnDelete();

            // For testing
            m_sl.LogData("(delete)");
            m_sl.LogData("Calibration Rating: " + m_ca.Rating);
            m_sl.AddNewLine();
        }

        private void Enter_Button_Click(object sender=null, EventArgs e=null)
        {
            usd.Append("\n");
            m_ca.UpdateAnalyzer(usd.String, ref m_predictedWords);
            m_ca.OnClear();

            // For testing
            m_sl.LogData("(enter)");
            m_sl.LogData("Calibration Rating: " + m_ca.Rating);
            m_sl.AddNewLine();
        }
        private void Space_Button_Click(object sender=null, EventArgs e=null)
        {
            usd.Append(" ");
            m_ca.UpdateAnalyzer(usd.String, ref m_predictedWords);

            // For testing
            m_sl.LogData("(space)");
            m_sl.LogData("Calibration Rating: " + m_ca.Rating);
            m_sl.AddNewLine();
        }

        private void Speak_btn_Click(object sender=null, RoutedEventArgs e=null)
        {
            Recalibration();
        }
        
        private void _btnBotLeft_Click(object sender=null, RoutedEventArgs e=null)
        {
            usd.CountDown++;
        }

        private void _btnBotRight_Click(object sender=null, RoutedEventArgs e=null)
        {
            if (usd.CountDown > 0)
            {
                usd.CountDown--;
            }
        }

        private void _Shift_Click(object sender=null, RoutedEventArgs e=null)
        {
            tmpBtnCollection.Clear();
            SetItemssourceOfKeyboardKeys();
            Dispatcher.BeginInvoke((Action)(() =>
            {
                CollectionAlltheButtonsInTheView();

            }), System.Windows.Threading.DispatcherPriority.Input);

            // For testing
            m_sl.LogData("(shift)");
            m_sl.AddNewLine();
        }

        private void Recalibration()
        {
            usd.OFF();
            Window w = new QBWindow.Recalibration(_timeToWaitBeforeRecalibrate, tmpBtnCollection, this, _eyeXHost);
            w.Show();
            w.Closing += usd.ON;
            w.Closed -= usd.ON;            
        }
        
        private void _TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // *** Clean the "time sensitive" variables from other tabitems ***
            if (_Timer != null)
            {
                _Timer.Stop();
            }

            GazingOnObj = null;
            // ****************************************************************
        }
    }
}