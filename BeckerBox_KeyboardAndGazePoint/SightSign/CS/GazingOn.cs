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

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private void inTobiiStreamFoundGazingElement(Control GazingOn)
        {
            if (GazingOn != null)
            {
                if (GazingOn is Button)
                {
                    GazingOn.Background = new SolidColorBrush(Colors.Aqua);
                    _Timer.Reset(GazingOn);
                    if (UtinityBtns.Contains(GazingOn))
                    {
                        if(ReferenceEquals(GazingOn, Space_Btn))
                        {
                            _Timer._pressIT += Space_Button_Click;
                        }

                        if (ReferenceEquals(GazingOn, _Clear))
                        {
                            _Timer._pressIT += _Clear_Click;
                        }

                        if (ReferenceEquals(GazingOn, Enter_btn))
                        {
                            _Timer._pressIT += Enter_Button_Click;
                        }

                        if (ReferenceEquals(GazingOn, Del_btn))
                        {
                            _Timer._pressIT += Delete_Button_Click;
                        }

                    }
                    else
                    {
                        _Timer._pressIT += Button_Click;
                    }
                    _Timer.Start();
                }
            }
        }

        private void CollectionAlltheButtonsInTheView()
        {
            tmpBtnCollection = new List<ControlsXYandWidthHeight>();
            foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>((MainWindow)System.Windows.Application.Current.MainWindow))
            {
                tmpBtnCollection.Add(new BeckerBox.ControlsXYandWidthHeight(btn, btn.PointToScreen(new Point(0d, 0d)).X, btn.PointToScreen(new Point(0d, 0d)).Y, btn.ActualWidth, btn.ActualHeight));
            }

        }

        private void FindUtinityBtns()
        {
            UtinityBtns = new ObservableCollection<Button>();
            foreach (Button btn in getAllControlsByType.FindVisualChildren<Button>((MainWindow)System.Windows.Application.Current.MainWindow))
            {
                UtinityBtns.Add(btn);
            }

        }

        private void setUpAllKeysStyle(object sender, EventArgs e)
        {
            for (int i = 0; i < keyboard_Rows.Count; i++)
            {
                keyboard_Rows[i].ItemTemplate = setUpTheKeysStyle(_keyBoard.Children.IndexOf((keyboard_Rows[i].Parent as Grid)));
            }

            Dispatcher.BeginInvoke((Action)(() =>
            {

                CollectionAlltheButtonsInTheView();

            }), System.Windows.Threading.DispatcherPriority.Normal);

        }
    }

    //Find the Control Elements based on the X and Y
    public static class GridExtensions
    {
        public static TControl GetElements<TControl>(this List<ControlsXYandWidthHeight> list, double row, double column)
            where TControl : Control
        {
            if (list != null)
            {
                var elements = from ControlsXYandWidthHeight element in list
                               where element.Self is TControl &&
                                     element.X <= row &&
                                     element.X + element.Width >= row &&
                                     element.Y + element.Height >= column &&
                                     element.Y <= column
                               select element.Self as TControl;

                if (elements != null)
                {
                    Collection<TControl> tmp = new Collection<TControl>(elements.ToList());

                    if (tmp.Count > 1)
                    {
                        throw new Exception("tmp.Count>1");
                    }

                    if (tmp.Count == 0)
                    {
                        return null;
                    }

                    return tmp[tmp.Count - 1];
                }

                return null;
            }

            return null;
        }
    }

    //get All Control Elements (such as, in XAML) By Type
    public static class getAllControlsByType
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }

    public class ControlsXYandWidthHeight
    {
        public Control Self { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        public ControlsXYandWidthHeight(Control self, double x, double y, double width, double height)
        {
            /*
            if(self == null | x < 0 | y < 0 | width <= 0 | height <= 0 | 
                Double.IsNaN(x) | Double.IsNaN(y) | Double.IsNaN(width) | Double.IsNaN(height) | 
                Double.IsPositiveInfinity(x) | Double.IsPositiveInfinity(y) | Double.IsPositiveInfinity(width) | Double.IsPositiveInfinity(height) |
                Double.IsNegativeInfinity(x) | Double.IsNegativeInfinity(y) | Double.IsNegativeInfinity(width) | Double.IsNegativeInfinity(height))
            {
                throw new ArgumentException("Arguments are not valid!!!!");
            }
            */  //<----- Don't need all those, since if the Controls are not visible, don't need to worry about it

            Self = self;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
