using System.Text;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tobii_Eris_Library
{
    public class UniversalString : INotifyPropertyChanged
    {
        #region Fields

        #region OnPropertyChanged Values

        public static String PropertyChanged_CountDown { get; private set; }
        public static String PropertyChanged_Append { get; private set; }
        public static String PropertyChanged_AppendFormat { get; private set; }
        public static String PropertyChanged_AppendLine { get; private set; }
        public static String PropertyChanged_Clear { get; private set; }
        public static String PropertyChanged_Insert { get; private set; }
        public static String PropertyChanged_Replace { get; private set; }
        public static String PropertyChanged_Remove { get; private set; }
        public static String PropertyChanged_String { get; private set; }
        public static String PropertyChanged_Length { get; private set; }
        public static String PropertyChanged_IndexOf { get; private set; }

        private void initAllPropertyChanged_Names()
        {
            PropertyChanged_CountDown = "CountDown";
            PropertyChanged_Append = "Append";
            PropertyChanged_AppendFormat = "AppendFormat";
            PropertyChanged_AppendLine = "AppendLine";
            PropertyChanged_Clear = "Clear";
            PropertyChanged_Insert = "Insert";
            PropertyChanged_Replace = "Replace";
            PropertyChanged_Remove = "Remove";
            PropertyChanged_String = "String";
            PropertyChanged_Length = "Length";
            PropertyChanged_IndexOf = "set IndexOf";
        }

        #endregion

        private StringBuilder Self;
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool RUN = true;
        #endregion

        #region Constructors

        public UniversalString()
        {
            initAllPropertyChanged_Names();
            Self = new StringBuilder();
        }

        public UniversalString(PropertyChangedEventHandler handler)
        {
            Self = new StringBuilder();
            PropertyChanged = handler;
            initAllPropertyChanged_Names();
        }

        public UniversalString(StringBuilder s)
        {
            Self = s;
            initAllPropertyChanged_Names();
        }

        public UniversalString(StringBuilder s, PropertyChangedEventHandler handler)
        {
            Self = s;
            PropertyChanged = handler;
            initAllPropertyChanged_Names();
        }

        #endregion

        #region Append

        public void Append(Boolean b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }

        public void Append(Byte b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Char b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Char b, Int32 i)
        {
            Self.Append(b, i);
            this.OnPropertyChanged(PropertyChanged_Append);
        }

        public void Append(Char[] b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Char[] b, Int32 i, Int32 i2)
        {
            Self.Append(b, i, i2);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Decimal b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Double b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Int16 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Int32 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Int64 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(object b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(SByte b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(Single b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(String b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(String b, Int32 i1, Int32 i2)
        {
            Self.Append(b, i1, i2);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(UInt16 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(UInt32 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }
        public void Append(UInt64 b)
        {
            Self.Append(b);
            this.OnPropertyChanged(PropertyChanged_Append);
        }


        #endregion

        #region AppendFormat
        public void AppendFormat(IFormatProvider s1, String s2, Object o)
        {
            Self.AppendFormat(s1, s2, o);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(IFormatProvider s1, String s2, Object o, Object o2)
        {
            Self.AppendFormat(s1, s2, o, o2);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(IFormatProvider s1, String s2, Object o, Object o2, Object o3)
        {
            Self.AppendFormat(s1, s2, o, o2, o3);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(IFormatProvider s1, String s2, Object[] o)
        {
            Self.AppendFormat(s1, s2, o);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(String s1, Object s2)
        {
            Self.AppendFormat(s1, s2);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(String s1, Object s2, Object s3)
        {
            Self.AppendFormat(s1, s2, s3);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(String s1, Object s2, Object s3, Object s4)
        {
            Self.AppendFormat(s1, s2, s3, s4);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }
        public void AppendFormat(String s1, Object[] s2)
        {
            Self.AppendFormat(s1, s2);
            this.OnPropertyChanged(PropertyChanged_AppendFormat);
        }

        #endregion

        #region AppendLine
        public void AppendLine()
        {
            Self.AppendLine();
            this.OnPropertyChanged(PropertyChanged_AppendLine);
        }

        public void AppendLine(String s)
        {
            Self.AppendLine(s);
            this.OnPropertyChanged(PropertyChanged_AppendLine);
        }

        #endregion

        public void Clear()
        {
            Self.Clear();
            this.OnPropertyChanged(PropertyChanged_Clear);
        }
        public void CopyeTo(Int32 i1, Char[] c, Int32 i2, Int32 i3)
        {
            Self.CopyTo(i1, c, i2, i3);
        }
        public int EnsureCapacity(Int32 i)
        {
            return Self.EnsureCapacity(i);
        }

        #region Insert
        public UniversalString Insert(Int32 i, Boolean s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Byte s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Char s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Char[] s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Char[] s, Int32 i1, Int32 i2)
        {
            StringBuilder tmp = Self.Insert(i, s, i1, i2);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Decimal s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Double s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Int16 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Int32 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Int64 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Object s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, SByte s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, Single s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, String s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, String s, Int32 i1)
        {
            StringBuilder tmp = Self.Insert(i, s, i1);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, UInt16 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, UInt32 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }
        public UniversalString Insert(Int32 i, UInt64 s)
        {
            StringBuilder tmp = Self.Insert(i, s);
            this.OnPropertyChanged(PropertyChanged_Insert);
            return new UniversalString(tmp);

        }


        #endregion

        #region Replace
        public UniversalString Replace(Char c1, Char c2)
        {
            StringBuilder tmp = Self.Replace(c1, c2);
            this.OnPropertyChanged(PropertyChanged_Replace);
            return new UniversalString(tmp);

        }

        public UniversalString Replace(Char c1, Char c2, Int32 i1, Int32 i2)
        {
            StringBuilder tmp = Self.Replace(c1, c2, i1, i2);
            this.OnPropertyChanged(PropertyChanged_Replace);
            return new UniversalString(tmp);

        }

        public UniversalString Replace(String c1, String c2)
        {
            StringBuilder tmp = Self.Replace(c1, c2);
            this.OnPropertyChanged(PropertyChanged_Replace);
            return new UniversalString(tmp);

        }

        public UniversalString Replace(String c1, String c2, Int32 i1, Int32 i2)
        {
            StringBuilder tmp = Self.Replace(c1, c2, i1, i2);
            this.OnPropertyChanged(PropertyChanged_Replace);
            return new UniversalString(tmp);

        }
        #endregion

        public UniversalString Remove(Int32 i, Int32 i2)
        {
            StringBuilder tmp = Self.Remove(i, i2);
            this.OnPropertyChanged(PropertyChanged_Remove);
            return new UniversalString(tmp);
        }

        #region ToString()

        public String String
        {
            get
            {
                return Self.ToString();
            }
            set
            {
                Self = new StringBuilder(value);
                OnPropertyChanged(PropertyChanged_String);
            }
        }

        public new String ToString()
        {
            return Self.ToString();
        }

        public String ToString(Int32 i1, Int32 i2)
        {
            return Self.ToString(i1, i2);
        }

        #endregion

        public int MaxCapacity
        {
            get
            {
                return Self.MaxCapacity;
            }
        }

        public int Length
        {
            get
            {
                return Self.Length;
            }

            set
            {
                Self.Length = value;
                this.OnPropertyChanged(PropertyChanged_Length);
            }
        }

        public int Capacity
        {
            get
            {
                return Self.Capacity;
            }

            set
            {
                Self.Capacity = value;
                //this.OnPropertyChanged("Capacity");
            }
        }

        #region Access based on Index
        public Char getIndexOf(Int32 index)
        {
            return Self[index];
        }

        public void setIndexOf(Int32 index, Char c)
        {
            Self[index] = c;
            OnPropertyChanged(PropertyChanged_IndexOf);
        }
        #endregion

        #region Ops (But only "+" so far, always return "String")
        //Universal and Universal
        public static UniversalString operator +(UniversalString first, UniversalString second)
        {
            return new UniversalString(new StringBuilder(first.Self.ToString() + second.Self.ToString()));
        }

        //Return String Always!!!!
        //string and Universal
        public static String operator +(String first, UniversalString second)
        {
            return first + second.Self.ToString();
        }

        //Return String Always!!!!
        //Universal and String
        public static String operator +(UniversalString first, String second)
        {
            return first.Self.ToString() + second;
        }
        #endregion

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null && RUN)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class UniversalDisplayString : UniversalString
    {
        #region Fields
        private uint _lengthLimit = 0;
        private uint _countDown = 0;
        public uint CountDown
        {
            get
            {
                return _countDown;
            }

            set
            {
                if (value >= 0 && _countDown <= Length)
                {
                    _countDown = value;
                }
                else if (value > Length)
                {
                    _countDown = (uint)Length;
                }
                else
                {
                    _countDown = 0;
                }

                base.OnPropertyChanged(PropertyChanged_CountDown);
            }

        }
        public uint CountLimit { get; set; }
        private String _Backup = null;
        #endregion

        #region Constructors

        public UniversalDisplayString(uint i) : base()
        {
            _lengthLimit = i;
        }

        public UniversalDisplayString(PropertyChangedEventHandler handler, uint i) : base(handler)
        {
            _lengthLimit = i;
        }

        public UniversalDisplayString(StringBuilder s, uint i) : base(s)
        {
            _lengthLimit = i;
        }

        public UniversalDisplayString(StringBuilder s, PropertyChangedEventHandler handler, uint i) : base(s, handler)
        {
            _lengthLimit = i;
        }

        #endregion
        public String Last(Int32 index)
        {
            if (_lengthLimit <= 0 | Length < _lengthLimit)
            {
                return String;
            }

            return String.Substring(0, index);
        }

        public List<Char> getCountDownCharItems()
        {
            if (Length < CountLimit)
            {
                return String.ToList();
            }

            if (Length - CountDown - CountLimit < 0) //leftmost to right
            {
                _countDown = (uint)(Length - CountLimit);
            }

            //rightmost to left

            return String.Substring((int)(Length - CountDown - CountLimit), (int)CountLimit).ToList();
        }

        public List<DisplayItems> getCountDownItems()
        {
            List<Char> tmp = getCountDownCharItems();
            List<DisplayItems> tmp2 = new List<DisplayItems>();

            uint i=0;

            if (Length - CountDown - CountLimit >= 0) //leftmost to right
            {
                i = (uint)(Length - CountDown - CountLimit);
            }

            foreach (Char x in tmp)
            {
                tmp2.Add(new DisplayItems(x, i));
                i++;
            }

            return tmp2;
        }

        public void OFF(object sender = null, EventArgs e = null)
        {
            _Backup = base.String;
            base.RUN = false;
        }

        public void ON(object sender = null, EventArgs e = null)
        {
            //Task.Delay(2000).ContinueWith(_ =>
            //{
            base.String = _Backup;
            base.RUN = true;
            //});

        }
    }


    public class DisplayItems
    {
        public Char Content { get; private set; }
        public uint Index { get; private set; }
        public DisplayItems(Char c, uint index)
        {
            Content = c;
            Index = index;
        }
    }
}
