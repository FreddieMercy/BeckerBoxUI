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

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // ********************************* Get the Elements **********************************
        private void CollectionAlltheButtonsInTheView()
        {
            if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(QWERTYKeyTab))
            {
                tmpBtnCollection.Clear();

                foreach(IsControlXYandWidthHeight x in OwnerBtns)
                {
                    tmpBtnCollection.Add(x);
                }

                foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>(BeckerBoxUI))
                {
                    btn.Focusable = false; //Use this for "eye-Enlarging"
                    tmpBtnCollection.Add(new IsControlXYandWidthHeight(btn));
                }
            }
        }

        private void CollectionMainBoxInTheView(object sender = null, EventArgs e = null)
        {
            if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(BeckerBoxTab))
            {
                Dispatcher.BeginInvoke((Action)(() =>
                {
                    MainBoxInTheView.Clear();

                    foreach (IsControlXYandWidthHeight x in OwnerBtns)
                    {
                        MainBoxInTheView.Add(x);
                    }

                    foreach (TextBlock btn in mMainBoardBoxes)
                    {
                        MainBoxInTheView.Add(new TextBlockControlXYandWidthHeight(btn));
                    }

                    foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>(BeckerBoxUI))
                    {
                        btn.Focusable = false; //Use this for "eye-Enlarging"
                        MainBoxInTheView.Add(new TextBlockControlXYandWidthHeight(btn));
                    }

                    _TabControl.SelectionChanged -= CollectionMainBoxInTheView;

                }), System.Windows.Threading.DispatcherPriority.Render);
            }
        }

        // ****************************************************************************************

        private void inTobiiStreamFoundGazingElement(UIElement GazingOn)
        {
            if (GazingOn != null)
            {
                _Timer.Stop();
                
                if (GazingOn is Button)
                {
                    (GazingOn as Button).Background = new SolidColorBrush(Colors.Aqua);

                    GazingOn.Focusable = true;

                    _Timer.Tick = (sender, e) =>
                    {
                        ButtonAutomationPeer peer = new ButtonAutomationPeer(GazingOn as Button);
                        IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;

                        if (GazingOn.IsEnabled && _eyeXHost.GazeTracking.Value == Tobii.EyeX.Framework.GazeTracking.GazeTracked)
                        {
                            invokeProv.Invoke();
                        }

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

        private void setUpAllKeysStyles_Normal(object sender = null, EventArgs e = null)
        {
            setUpAllKeysStyle_helper(System.Windows.Threading.DispatcherPriority.Normal);
        }

        private void setUpAllKeysStyles_Loaded(object sender = null, EventArgs e = null)
        {
            takingInput = false;
            updateData(System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private void setUpAllKeysStyle_helper (System.Windows.Threading.DispatcherPriority e = System.Windows.Threading.DispatcherPriority.Normal)
        {
            setUpAllKeysAssisst();
            updateData(e);
        }

        private void setUpAllKeysAssisst(object sender = null, EventArgs e = null)
        {
            takingInput = false;
            for (int i = 0; i < keyboard_Rows.Count; i++)
            {
                keyboard_Rows[i].ItemTemplate = setUpTheKeysStyle(_keyBoard.Children.IndexOf((keyboard_Rows[i].Parent as Grid)));
            }

            usd.CountLimit = (uint)(_tbBotLeft.ActualWidth / (_btnBotLeft.ActualHeight - halfMargin));
            _tbBotLeft.ItemTemplate = setUpTheDisplayingStyle();
        }

        private void updateData(System.Windows.Threading.DispatcherPriority e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                CollectionAlltheButtonsInTheView();
                takingInput = true;
            }), e);
        }

        private void QWERTYSelected(object sender = null, EventArgs e = null)
        {
            if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(QWERTYKeyTab))
            {
                setUpAllKeysStyle_helper(System.Windows.Threading.DispatcherPriority.Render);
            }
        }

        private void BBSelected(object sender = null, EventArgs e = null)
        {
            if (_TabControl.SelectedIndex == _TabControl.Items.IndexOf(BeckerBoxTab))
            {
                currentDot.Visibility = Visibility.Visible;
            }
            else
            {
                currentDot.Visibility = Visibility.Hidden;
            }
        }
    }

}
