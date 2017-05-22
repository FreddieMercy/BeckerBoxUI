using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Text;

namespace BeckerBox.QBWindow
{
    /// <summary>
    /// Interaction logic for QWERTYUI.xaml
    /// </summary>
    public partial class QWERTYUI : BeckerBoxUI, INotifyPropertyChanged
    {
        private void _Minus_EventHandlers_prepareForTheInsertIntoUsd()
        {
            usd.PropertyChanged -= setUpAllKeysStyles_Loaded;
            usd.PropertyChanged += _tmpKeyboardDisplatingSubString;
            usd.PropertyChanged -= keyboardDisplatingString;
            usd.PropertyChanged -= setUpTheSourceOfDisplayKey;
            Speak_btn.Click -= Speak_btn_Click; //Replace
        }
        private void _Plus_EventHandlers_prepareForTheInsertIntoUsd()
        {
            usd.PropertyChanged += setUpAllKeysStyles_Loaded;
            usd.PropertyChanged -= _tmpKeyboardDisplatingSubString;
            usd.PropertyChanged += keyboardDisplatingString;
            usd.PropertyChanged += setUpTheSourceOfDisplayKey;
            Speak_btn.Click += Speak_btn_Click; //Replace
        }

        private List<object> _Minus_UIElementsAndInfo_prepareForTheInsertIntoUsd(Button sender)
        {
            List<object> collect = new List<object>();

            collect.Add(usd.String);        //old string
            collect.Add(sender.Tag);        //Index
            return collect;
        }
        private void _Plus_UIElementsAndInfo_prepareForTheInsertIntoUsd(Button btn)
        {
            StringBuilder s3 = new StringBuilder((string)(btn.Tag as List<object>)[0]);
            int index = Int32.Parse((btn.Tag as List<object>)[1].ToString());
            s3.Remove(index, 1);
            s3.Insert(index, usd.String);
            usd.String = s3.ToString();
        }

        private void _Minus_ButtonStyle_prepareForTheInsertIntoUsd(Button sender)
        {
            sender.Tag = _Minus_UIElementsAndInfo_prepareForTheInsertIntoUsd(sender as Button);
            DoneBtn.Tag = sender;
            sender.Visibility = Visibility.Hidden;
            Speak_btn.IsEnabled = false;
        }

        private void _Plus_ButtonStyle_prepareForTheInsertIntoUsd(Button sender)
        {
            sender.Visibility = Visibility.Visible;
            Speak_btn.IsEnabled = true;
        }

        private void _Minus_DoneButtonSetup()
        {
            DoneBtn.Visibility = Visibility.Hidden;
            _btnBotGrid.Visibility = Visibility.Visible;
            Panel.SetZIndex(DoneBtn, 0);
            Panel.SetZIndex(_btnBotGrid, 1);
        }
        private void _Plus_DoneButtonSetup()
        {
            DoneBtn.Visibility = Visibility.Visible;
            _btnBotGrid.Visibility = Visibility.Hidden;
            Panel.SetZIndex(DoneBtn, 1);
            Panel.SetZIndex(_btnBotGrid, 0);
        }

        private DataTemplate setUpTheDisplayingStyle()
        {
            //ListBox, sort of
            DataTemplate template = new DataTemplate();

            //Button
            FrameworkElementFactory btn = new FrameworkElementFactory(typeof(Button));

            btn.SetBinding(Button.ContentProperty, new Binding("Content"));
            btn.SetBinding(Button.TagProperty, new Binding("Index"));

            btn.SetValue(Button.HeightProperty, _btnBotLeft.ActualHeight);
            btn.SetValue(Button.WidthProperty, _btnBotLeft.ActualHeight);

            btn.SetValue(Button.MarginProperty, new Thickness(0, 0, margin, 0));
            btn.SetValue(Button.PaddingProperty, new Thickness(0));
            //btn.SetValue(Button.FocusableProperty, false);
            btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(DisplayingClickHandler));

            template.VisualTree = btn;

            return template;
        }
    }
}