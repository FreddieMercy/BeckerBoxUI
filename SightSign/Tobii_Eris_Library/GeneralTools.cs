using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tobii_Eris_Library
{
    public static class GeneralTools
    {
        public enum ComparisonResult
        {
            LessThan,
            EqualTo,
            GreaterThan
        }

        public static bool CheckFileExists(string path)
        {
            if (File.Exists(path))
                return true;

            return false;
        }
    }
}
