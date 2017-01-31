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
        private Hashtable table = new Hashtable(); //save all the sorted keys based on their row and col indexes.
        private Keys k; //it saves all the key initiated, but before sorting
        private List<ListBox> keyboard_Rows = new List<ListBox>(); //add each row of keyboard which contains the keys, in sequence

        private void Settings()
        {
            margin = 10; //set the margin / height in xaml, for the purpose of keeping it consist

            otherMarginInit(); //Only reason why I put all those int method is for saving space. Don't need to care if "margin" exists. The definition is in "Methods.cs"
            AddRows();
            AddKeys();
            
        }

        private void AddRows()
        {
            //****** add each row of keyboard which contains the keys in sequence
            keyboard_Rows.Add(_Keyboard_FirstRow);
            keyboard_Rows.Add(_Keyboard_SecondRow);
            keyboard_Rows.Add(_Keyboard_ThirdRow);
            keyboard_Rows.Add(_Keyboard_ForthRow);

            //****** end adding keys into the rows
        }

        private void AddKeys()
        {
            //------ init all keys

            //*** First row
            k = new Keys("`", "~", 0, 1);
            k = new Keys("1", "!", 0, 2);
            k = new Keys("2", "@", 0, 3);

            k = new Keys("3", "#", 0, 4);
            k = new Keys("4", "$", 0, 5);
            k = new Keys("5", "%", 0, 6);

            k = new Keys("6", "^", 0, 7);
            k = new Keys("7", "&", 0, 8);
            k = new Keys("8", "*", 0, 9);
        
            k = new Keys("9", "(", 0, 10);
            k = new Keys("0", ")", 0, 11);
            k = new Keys("-", "_", 0, 12);

            k = new Keys("=", "+", 0, 13);

            //*** Second row
            k = new Keys("q", "Q", 1, 13); //reverse the order for better layout
            k = new Keys("w", "W", 1, 12);
            k = new Keys("e", "E", 1, 11);

            k = new Keys("r", "R", 1, 10);
            k = new Keys("t", "T", 1, 9);
            k = new Keys("y", "Y", 1, 8);

            k = new Keys("u", "U", 1, 7);
            k = new Keys("i", "I", 1, 6);
            k = new Keys("o", "O", 1, 5);
        
            k = new Keys("p", "P", 1, 4);
            k = new Keys("]", "}", 1, 3);
            k = new Keys("[", "{", 1, 2);
            k = new Keys("\\", "|", 1, 1);

            //*** Third row
            k = new Keys("a", "A", 2, 1);
            k = new Keys("s", "S", 2, 2);
            k = new Keys("d", "D", 2, 3);

            k = new Keys("f", "F", 2, 4);
            k = new Keys("g", "G", 2, 5);
            k = new Keys("h", "H", 2, 6);
       
            k = new Keys("j", "J", 2, 7);
            k = new Keys("k", "K", 2, 8);
            k = new Keys("l", "L", 2, 9);

            k = new Keys(";", ":", 2, 10);
            k = new Keys("'", "\"", 2, 11);

            //*** Forth row
            k = new Keys("z", "Z", 3, 1);
            k = new Keys("x", "X", 3, 2);
            k = new Keys("c", "C", 3, 3);
        
            k = new Keys("v", "V", 3, 4);
            k = new Keys("b", "B", 3, 5);
            k = new Keys("n", "N", 3, 6);

            k = new Keys("m", "M", 3, 7);
            k = new Keys(",", "<", 3, 8);
            k = new Keys(".", ">", 3, 9);

            k = new Keys("/", "?", 3, 10);

            //------ end init keys
        }
    }
}