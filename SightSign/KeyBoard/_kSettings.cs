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
using System.Text;
using Tobii_Eris_Library;

namespace BeckerBox
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //Const values
                
        private const string _DisplayAndInsertKeyBoardTextBeginningString="[Please insert: ]";
        private const string endingCode = "♦";
        private const uint _characterLimit = 200;
        //private PointXY _currentStreamData = new PointXY(0.0, 0.0);
        // For testing
        StatisticsLogger m_sl;
        //******   For "GazingOn"   ************
        private Brush btnColor;
        //**************************************

        private Hashtable table = new Hashtable(); //save all the sorted keys based on their row and col indexes.
        private Keys k; //it saves all the key initiated, but before sorting
        private List<ItemsControl> keyboard_Rows = new List<ItemsControl>(); //add each row of keyboard which contains the keys, in sequence
        private List<ControlsXYandWidthHeight> tmpBtnCollection = new List<ControlsXYandWidthHeight>();
        private CalibrationAnalyzer m_ca = new CalibrationAnalyzer();
        private List<string> m_predictedWords = new List<string>();
        
        private void SettingsForKeyboard()
        {
            margin = 10; //set the margin / height in xaml, for the purpose of keeping it consist
            
            TabHeight = 50;
            TabWidth = 100;
            tabFontSize = 24;            

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
            k = new Keys("`", "~", 0, 1, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("1", "!", 0, 2, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("2", "@", 0, 3, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("3", "#", 0, 4, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("4", "$", 0, 5, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("5", "%", 0, 6, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("6", "^", 0, 7, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("7", "&", 0, 8, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("8", "*", 0, 9, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("9", "(", 0, 10, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("0", ")", 0, 11, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("-", "_", 0, 12, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("=", "+", 0, 13, _keyBoard.RowDefinitions.Count, _Shift);

            //*** Second row
            k = new Keys("q", "Q", 1, 13, _keyBoard.RowDefinitions.Count, _Shift); //reverse the order for better layout
            k = new Keys("w", "W", 1, 12, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("e", "E", 1, 11, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("r", "R", 1, 10, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("t", "T", 1, 9, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("y", "Y", 1, 8, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("u", "U", 1, 7, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("i", "I", 1, 6, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("o", "O", 1, 5, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("p", "P", 1, 4, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("]", "}", 1, 3, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("[", "{", 1, 2, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("\\", "|", 1, 1, _keyBoard.RowDefinitions.Count, _Shift);

            //*** Third row
            k = new Keys("a", "A", 2, 1, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("s", "S", 2, 2, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("d", "D", 2, 3, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("f", "F", 2, 4, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("g", "G", 2, 5, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("h", "H", 2, 6, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("j", "J", 2, 7, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("k", "K", 2, 8, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("l", "L", 2, 9, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys(";", ":", 2, 10, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("'", "\"", 2, 11, _keyBoard.RowDefinitions.Count, _Shift);

            //*** Forth row
            k = new Keys("z", "Z", 3, 1, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("x", "X", 3, 2, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("c", "C", 3, 3, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("v", "V", 3, 4, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("b", "B", 3, 5, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys("n", "N", 3, 6, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("m", "M", 3, 7, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys(",", "<", 3, 8, _keyBoard.RowDefinitions.Count, _Shift);
            k = new Keys(".", ">", 3, 9, _keyBoard.RowDefinitions.Count, _Shift);

            k = new Keys("/", "?", 3, 10, _keyBoard.RowDefinitions.Count, _Shift);

            //------ end init keys
        }
        
    }
}