using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private class MathHelper
        {
            internal class Point2D
            {
                internal double X { get; set; }
                internal double Y { get; set; }

                internal Point2D()
                {
                    X = 0;
                    Y = 0;
                }

                internal Point2D(double x, double y)
                {
                    X = x;
                    Y = y;
                }

                internal Point2D(int x, int y)
                {
                    X = Convert.ToDouble(x);
                    Y = Convert.ToDouble(y);
                }
            };

            internal class Line
            {
                internal double Slope { get; private set; }
                internal double YIntercept { get; private set; }

                internal Line(double slope, double yIntercept)
                {
                    Slope = slope;
                    YIntercept = yIntercept;
                }

                internal double EvalX(double x)
                {
                    return (Slope * x) + YIntercept;
                }
            };

            //Gets line equation from two given points. Returns a simple y = mx + b equation by returning the slope and the y-intercept
            internal static Line GetLineEquation(Point2D firstPoint, Point2D secondPoint)
            {
                double rise = firstPoint.Y - secondPoint.Y;
                double run = firstPoint.X - secondPoint.X;

                //If the denominator (the run) is 0, return
                if (run >= -0.0001 && run <= 0.0001)
                {
                    return null;
                }

                double slope = rise / run;

                //in y = mx + b, the slope is the m value. To find b, we use the equation y - mx = b.
                double yIntercept = secondPoint.Y - (slope * secondPoint.X);

                return new Line(slope, yIntercept);
            }
        }
    }
}
