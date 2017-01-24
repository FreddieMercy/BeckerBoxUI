using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;

namespace BeckerBoxForEyeGaze
{
    //this file contains all the cursor related methods.
    public partial class MainWindow : Window
    {
        private void cursorLocalization(object sender, EventArgs e)
        {
            _cursorLocTB.Text = this.GetMousePositionWindowsForms().X + "," + this.GetMousePositionWindowsForms().Y;
        }

        //return the x and y coord of cursor
        private System.Drawing.Point GetMousePositionWindowsForms()
        {
            System.Drawing.Point point = Control.MousePosition;
            return new System.Drawing.Point(point.X, point.Y);
        }
    }
}
