using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tobii_Eris_Library
{
    /*************************************IMPORANT NOTE**************************************
     * MAKE SURE the file "google-10000-english-usa.txt" is in the DictionaryFiles as
     * well as "20k.txt"
     * folder! Make sure under Project->Properties->Build Events,
     * the Pre-Build event contains these lines:
     * copy .\..\..\..\DictionaryFiles\google-10000-english-usa.txt .\
     * copy .\..\..\..\DictionaryFiles\20k.txt .\
    ****************************************************************************************/

    /*************************************GENERAL INFO***************************************
     * Be sure to check out the information in the WordBank.cs file. Important information
     * is included in there if you want to override certain functions.
     * 
     * Original source for "google-10000-english-usa.txt":
     * https://raw.githubusercontent.com/first20hours/google-10000-english/master/google-10000-english-usa.txt
     * Original source for "20k.txt":
     * https://raw.githubusercontent.com/first20hours/google-10000-english/master/20k.txt
    ****************************************************************************************/
    public class EnglishWordBank : WordBank
    {
        public EnglishWordBank() : base("20k.txt", false)
        {

        }
    }
}
