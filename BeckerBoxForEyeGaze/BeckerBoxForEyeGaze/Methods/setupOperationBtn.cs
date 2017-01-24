using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BeckerBoxForEyeGaze
{
    public partial class MainWindow : Window
    {
        //this file contains the methods that had been used to help the methods in initialization (methods in "setUpBtn.cs")
        private int findGreater(int i, int j)
        {
            if (i > j)
            {
                return i;
            }
            else
            {
                return j;
            }
        }

        private int appendArray(ref string[] dest, string[] source)
        {
            int array1OriginalLength = dest.Length;
            Array.Resize<string>(ref dest, array1OriginalLength + source.Length);
            Array.Copy(source, 0, dest, array1OriginalLength, source.Length);
            return array1OriginalLength + source.Length;

        }

        private Button setupPrevious()
        {
            List<string> addon = new List<string>();
            int max = 0, val;

            max = findGreater(btnUpperLeft.Length, btnUpperRight.Length);
            max = findGreater(max, btnBotLeft.Length);
            max = findGreater(max, btnBotRight.Length);

            for(int i = btnUpperLeft.Length; i < max; i++)
            {
                addon.Add(btnUpperLeft[0]);
            }

            val = appendArray(ref btnUpperLeft, addon.ToArray());

            addon.Clear();

            for (int i = btnUpperRight.Length; i < max; i++)
            {
                addon.Add(btnUpperRight[0]);
            }

            val = appendArray(ref btnUpperRight, addon.ToArray());

            addon.Clear();

            for (int i = btnBotLeft.Length; i < max; i++)
            {
                addon.Add(btnBotLeft[0]);
            }

            val = appendArray(ref btnBotLeft, addon.ToArray());

            addon.Clear();

            for (int i = btnBotRight.Length; i < max; i++)
            {
                addon.Add(btnBotRight[0]);
            }

            val = appendArray(ref btnBotRight, addon.ToArray());

            addon.Clear();


            string[] tmp = btnUpperLeft;

            int array1OriginalLength = tmp.Length;

            Array.Resize<string>(ref tmp, array1OriginalLength + btnUpperRight.Length);
            Array.Copy(btnUpperRight, 0, tmp, array1OriginalLength, btnUpperRight.Length);
            array1OriginalLength += btnUpperRight.Length;

            Array.Resize<string>(ref tmp, array1OriginalLength + btnBotLeft.Length);
            Array.Copy(btnBotLeft, 0, tmp, array1OriginalLength, btnBotLeft.Length);
            array1OriginalLength += btnBotLeft.Length;

            Array.Resize<string>(ref tmp, array1OriginalLength + btnBotRight.Length);
            Array.Copy(btnBotRight, 0, tmp, array1OriginalLength, btnBotRight.Length);

            return new Button() { DataContext = tmp };
        }
    }
}
