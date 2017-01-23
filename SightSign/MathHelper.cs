using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeckerBox
{
    public class MathHelper
    {
        public class Point2D
        {
            public double X;
            public double Y;

            public Point2D()
            {
                X = 0;
                Y = 0;
            }

            public Point2D(double x, double y)
            {
                X = x;
                Y = y;
            }

            public Point2D(int x, int y)
            {
                X = Convert.ToDouble(x);
                Y = Convert.ToDouble(y);
            }
        };

        public class Line
        {
            public double Slope;
            public double YIntercept;

            public Line(double slope, double yIntercept)
            {
                Slope = slope;
                YIntercept = yIntercept;
            }

            public double EvalX(double x)
            {
                return (Slope * x) + YIntercept;
            }
        };

        //Gets line equation from two given points. Returns a simple y = mx + b equation by returning the slope and the y-intercept
        public static Line GetLineEquation(Point2D firstPoint, Point2D secondPoint)
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
