using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tobii_Eris_Library;
using System.Text;

namespace BeckerBox
{
    /// <summary>
    /// Interaction logic for BeckerBoxUI.xaml
    /// </summary>
    public partial class BeckerBoxUI : System.Windows.Controls.UserControl
    {
        private MainWindow tmp;
        private List<System.Windows.Controls.Button> thisBtns = new List<System.Windows.Controls.Button>();
        private StatisticsLogger m_sl;// = new StatisticsLogger("qwertyTestResults.txt", "QWERTY Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nQWERTY Keyboard Hover-To-Click time: " + _timerInterval + "ms"));
        private StatisticsLogger m_s2;// = new StatisticsLogger("bbTestResults.txt", "BB Keyboard Test Results", new StringBuilder("Tested By: Alex Kerr\r\nBeckerBox Keyboard Hover-To-Click time: " + _timerInterval + "ms"));

        // temp
        private string tester = "Patrick Guo";

        public BeckerBoxUI()
        {
            InitializeComponent();

            //Add existing buttons in this interface

            thisBtns.Add(QwertBtn);
            thisBtns.Add(BBBtn);

            m_sl = new StatisticsLogger("qwertyTestResults.txt", "QWERTY Keyboard Test Results", new StringBuilder("Tested By:" + tester +"\r\nQWERTY Keyboard Hover-To-Click time: " + MainWindow._timerInterval + "ms"));
            m_s2 = new StatisticsLogger("bbTestResults.txt", "BB Keyboard Test Results", new StringBuilder("Tested By:" + tester + "\r\nBeckerBox Keyboard Hover-To-Click time: " + MainWindow._timerInterval + "ms"));

            ifManuallyClosed();
        }

        private void cleanClose(object sender = null, EventArgs e = null)
        {
            tmp.Closed -= ifManuallyClosed;
            tmp.Close();

            //close writers
            m_sl.WriteLoggerDetails();
            m_s2.WriteLoggerDetails();
        }

        private void ifManuallyClosed(object sender = null, EventArgs e =null)
        {
            tmp = new MainWindow(thisBtns, this, m_sl, m_s2);

            tmp.StateChanged += reverseWindowSizeChangeEffects;

            if (Parent != null)
            {
                Unloaded -= cleanClose;
                ((((Parent as TabItem).Parent as TabControl).Parent as Grid).Parent as Window).Closing += cleanClose;
            }
            else
            {
                Unloaded += cleanClose;
            }

            tmp.Closed += ifManuallyClosed;
            tmp._TabControl.SelectedIndex = 0;  //Since the QWERTY uses a lot of "Binding" attributes (i.e: margin), 
                                                //thus make it the first one to launch becuase otherwise those attributes cannot be rendered.(WPF Window weird design)

            //reminder: When minimizing the window, also enable all the buttons in this interface

            QwertBtn.IsEnabled = true;
            BBBtn.IsEnabled = true;
        }

        //reverse all the effect to other windows if THIS window's state (Minimized, Maximumized, Normal) changed
        private void reverseWindowSizeChangeEffects(object sender, EventArgs e = null)
        {
            if((sender as Window).WindowState == WindowState.Minimized)
            {
                QwertBtn.IsEnabled = true;
                BBBtn.IsEnabled = true;
            }
        }

        private void WindowControlling(int index)
        {
            if (!tmp.IsLoaded)
            {
                tmp.Close();
                tmp.Show();
            }
            
            tmp.WindowState = WindowState.Maximized;
            tmp.SelectedIndex = index;

            //Disable all the buttons in this interface
            QwertBtn.IsEnabled = false;
            BBBtn.IsEnabled = false;
        }

        private void Qwert(object sender, RoutedEventArgs e)
        {
            WindowControlling(tmp._TabControl.Items.IndexOf(tmp.QWERTYKeyTab));
        }

        private void BB(object sender, RoutedEventArgs e)
        {
            WindowControlling(tmp._TabControl.Items.IndexOf(tmp.BeckerBoxTab));
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            tmp.Close();
        }
    }
}

