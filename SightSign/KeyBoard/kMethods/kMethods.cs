using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Tobii_Eris_Library;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void SortKeys()
        {
            table.Clear();
            //init the item_collection based on rowIndex
            for (int i = 0; i < _keyBoard.RowDefinitions.Count; i++)
            {
                table[i] = new SortedSet<Tuple<int, Keys>>();
            }

            //sort each items_collection based on the columnIndex
            foreach (Keys x in Keys.Items)
            {
                (table[Keys.Items[Keys.Items.IndexOf(x)].RowIndex] as SortedSet<Tuple<int, Keys>>).Add(new Tuple<int, Keys>(Keys.Items[Keys.Items.IndexOf(x)].ColIndex, x));
            }
        }

        private void SetItemssourceOfKeyboardKeys()
        {
            for (int i = 0; i < keyboard_Rows.Count; i++)
            {
                List<Keys> sortedKeys = new List<Keys>();

                foreach (Tuple<int, Keys> x in (table[i] as SortedSet<Tuple<int, Keys>>))
                {
                    sortedKeys.Add(x.Item2);
                }

                keyboard_Rows[i].ItemsSource = sortedKeys;
            }            
        }

        private DataTemplate setUpTheKeysStyle(int i)
        {
            //ListBox, sort of
            DataTemplate template = new DataTemplate();

            //Button
            FrameworkElementFactory btn = new FrameworkElementFactory(typeof(Button));

            btn.SetBinding(Button.ContentProperty, new Binding("Content"));

            btn.SetValue(Button.HeightProperty, _keyBoard.RowDefinitions[i].ActualHeight);
            btn.SetValue(Button.WidthProperty, _keyBoard.RowDefinitions[i].ActualHeight);

            btn.SetValue(Button.MarginProperty, new Thickness(0, 0, margin, 0));
            btn.SetValue(Button.PaddingProperty, new Thickness(0));
            //btn.SetValue(Button.FocusableProperty, false);
            btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));

            template.VisualTree = btn;

            return template;
        }
        
        private static object GetValueFromStyle(object styleKey, DependencyProperty property)
        {
            Style style = Application.Current.TryFindResource(styleKey) as Style;
            while (style != null)
            {
                var setter =
                    style.Setters
                        .OfType<Setter>()
                        .FirstOrDefault(s => s.Property == property);

                if (setter != null)
                {
                    return setter.Value;
                }

                style = style.BasedOn;
            }
            return null;
        }
        
        private void otherMarginInit()
        {
            //Don't need to care if "margin" exists
            halfMargin = margin / 2;
            halfMargin_value = margin + "," + halfMargin + ",0," + halfMargin;
            halfMargin_right = halfMargin + "," + halfMargin + "," + margin + "," + halfMargin;
            margin_right="0,0,"+margin +",0";
            this.DataContext = this; //do not modify it if "margin" exists
            //_______________
        }

        // for setting the value of "margin / height" in xaml
        //****** You don't need to care about what below, if "margin" exists

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public int _TabWidth = 0;
        public int _TabHeight = 0;
        public int _margin = 0;
        public int _tabFontSize = 0;

        public int tabFontSize
        {
            get { return _tabFontSize; }
            set
            {
                _tabFontSize = value;
                //Notify the binding that the value has changed.
                this.OnPropertyChanged("tabFontSize");
            }
        }
        public int TabWidth
        {
            get { return _TabWidth; }
            set
            {
                _TabWidth = value;
                //Notify the binding that the value has changed.
                this.OnPropertyChanged("TabWidth");
            }
        }

        public int TabHeight
        {
            get { return _TabHeight; }
            set
            {
                _TabHeight = value;
                //Notify the binding that the value has changed.
                this.OnPropertyChanged("TabHeight");
            }
        }

        public int margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                //Notify the binding that the value has changed.
                this.OnPropertyChanged("margin");
            }
        }

        public int _halfMargin;
        public int halfMargin
        {
            get { return _halfMargin; }
            set
            {
                _halfMargin = value;
                this.OnPropertyChanged("halfMargin");
            }
        }

        public string _halfMargin_value;
        public string halfMargin_value
        {
            get { return _halfMargin_value; }
            set
            {
                _halfMargin_value = value;
                this.OnPropertyChanged("halfMargin_value");
            }
        }

        public string _halfMargin_right;
        public string halfMargin_right
        {
            get { return _halfMargin_right; }
            set
            {
                _halfMargin_right = value;
                this.OnPropertyChanged("halfMargin_right");
            }
        }

        public string _margin_right;
        public string margin_right
        {
            get { return _margin_right; }
            set
            {
                _margin_right = value;
                this.OnPropertyChanged("margin_right");
            }
        }

        //****** End of "margin"
    }
}
