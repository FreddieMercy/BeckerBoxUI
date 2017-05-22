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
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Diagnostics;
using System.Threading.Tasks;
using Tobii_Eris_Library;


namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for BBUI.xaml
    /// </summary>
    public partial class BBUI : BeckerBoxUI
    {
        // ********************************* Get the Elements **********************************
        private void bbDisplatingString(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != UniversalDisplayString.PropertyChanged_CountDown)
            {
                TextQueueTextBox.Text = (sender as UniversalDisplayString).String;
            }
        }

        private void CollectionMainBoxInTheView(object sender = null, EventArgs e = null)
        {

                MainBoxInTheView.Clear();
                foreach (TextBlock btn in mMainBoardBoxes)
                {
                    MainBoxInTheView.Add(new IsNotControlXYandWidthHeight(btn, btn.PointToScreen(new Point(0d, 0d)).X, btn.PointToScreen(new Point(0d, 0d)).Y, btn.ActualWidth, btn.ActualHeight, Panel.GetZIndex(btn)));
                }

                foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>((MainWindow)System.Windows.Application.Current.MainWindow))
                {
                    btn.Focusable = false; //Use this for "eye-Enlarging"
                    MainBoxInTheView.Add(new IsNotControlXYandWidthHeight(btn, btn.PointToScreen(new Point(0d, 0d)).X, btn.PointToScreen(new Point(0d, 0d)).Y, btn.ActualWidth, btn.ActualHeight, Panel.GetZIndex(btn)));
                }
            
        }

        private void inTobiiStreamFoundGazingElement(UIElement GazingOn)
        {
            if (GazingOn != null)
            {
                _Timer.Reset(GazingOn);

                if (GazingOn is Button)
                {
                    (GazingOn as Button).Background = new SolidColorBrush(Colors.Aqua);

                    GazingOn.Focusable = true;

                    _Timer._pressIT += (sender, e) =>
                    {

                        ButtonAutomationPeer peer = new ButtonAutomationPeer(GazingOn as Button);
                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                        invokeProv.Invoke();

                        (GazingOn as Button).Background = new SolidColorBrush(Colors.Green);

                    };

                    _Timer.Start();//Only place that "_Timers" for "QWERTY Keyboard", if anyone added others please mark
                }
                else if (GazingOn is TextBlock)
                {
                    MouseEnterBox(GazingOn, null);
                }
            }
        }
    }

}
