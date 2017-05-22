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
    /// Interaction logic for QWERTYUI.xaml
    /// </summary>
    public partial class QWERTYUI : BeckerBoxUI, INotifyPropertyChanged
    {
        // ********************************* Get the Elements **********************************
        private void CollectionAlltheButtonsInTheView()
        {
            tmpBtnCollection.Clear();
            foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>((MainWindow)System.Windows.Application.Current.MainWindow))
            {
                btn.Focusable = false; //Use this for "eye-Enlarging"
                tmpBtnCollection.Add(new IsControlXYandWidthHeight(btn));
            }
        }

        private void inTobiiStreamFoundGazingElement(UIElement GazingOn)
        {
            if (GazingOn != null)
            {
                _Timer.Reset(GazingOn);

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
        }

        private void setUpAllKeysStyles_Normal(object sender, EventArgs e)
        {
            setUpAllKeysStyle_helper(System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void setUpAllKeysStyles_Loaded(object sender, EventArgs e)
        {
            takingInput = false;
            updateData(System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private void setUpAllKeysStyle_helper (System.Windows.Threading.DispatcherPriority e = System.Windows.Threading.DispatcherPriority.Normal)
        {
            takingInput = false;
            for (int i = 0; i < keyboard_Rows.Count; i++)
            {
                keyboard_Rows[i].ItemTemplate = setUpTheKeysStyle(_keyBoard.Children.IndexOf((keyboard_Rows[i].Parent as Grid)));
            }
            
            usd.CountLimit = (uint)(_tbBotLeft.ActualWidth / (_btnBotLeft.ActualHeight - halfMargin));            
            _tbBotLeft.ItemTemplate = setUpTheDisplayingStyle();
            updateData(e);
        }

        private void updateData(System.Windows.Threading.DispatcherPriority e) 
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                CollectionAlltheButtonsInTheView();
                takingInput = true;
            }), e);
        }
    }

}
