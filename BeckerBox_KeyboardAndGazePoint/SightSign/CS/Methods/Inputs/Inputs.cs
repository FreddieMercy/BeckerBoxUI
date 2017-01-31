using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeckerBox
{
    public class Inputs
    {
        public string Content { get; protected set; } //The reason why I am using "string" instead of "char" is, seeking the possibility that user can input words (i.e: place, time, and personal pronoun）
                                                      //which should be more convenient for the user comparing to input single "char" each time 

        public Inputs(string content)
        {
            if(string.IsNullOrEmpty(content))
            {
                throw new NullReferenceException("Inputs(string.IsNullOrEmpty(content))");
            }

            Content = content;
        }
    }
}
