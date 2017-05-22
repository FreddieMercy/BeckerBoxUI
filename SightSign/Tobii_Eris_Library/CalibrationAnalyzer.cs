using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tobii_Eris_Library
{
    /*************************************IMPORANT NOTE**************************************
     * READ the IMPORANT NOTE in EnglishWordBank.cs!
     ****************************************************************************************/

    /*********************************HOW TO USE THIS CLASS***********************************
     * (1) Call UpdateAnalyzer(s, list) for every time you update the UI's textbox's text to 
     * retrieve the calibration rating.
     * 
     * (2) Call OnClear() for every time you clear(or "send") the UI textbox's text.
     * 
     * (3) Call ResetRating() for every time after you finish re-calibrating the calibration.
     ****************************************************************************************/

    /*************************************GENERAL INFO***************************************
     * How the analyzer works is: 
     * This analyzer holds 1 to 10 scale rating and an english word bank. We also have a
     * string that references the previous text that got analyzed to determine if
     * the current text is being "deleted" with a delete key. 
     * 
     * A rating of 8 to 10 means it's properly calibrated.
     * A rating of 1 to 7 means it's BADLY calibrated.
     * 
     * This analyzer mainly relies on the user to use the UpdateAnalyzer(s, list) function.
     * Each time the UpdateAnalyzer(s, list) function gets called, the CalculateRating()
     * function, a function which processes an algorithm to estimate the updated calibration
     * rating, gets called automatically
     * 
     * How to use this class:
     * (1) Read the IMPORTANT NOTE in the above comment
     * (2) Make an instance of this class somewhere. The constructor takes no arguments. 
     * (3) Call the UpdateAnalyzer(s, list) function every time you update the main UI textbox. 
     * (4) Call the ResetRating function every time after you re-calibrate
     * More information for the above 2 functions can be found in the comments posted
     * above the function definitions.
     * 
     * Future Considerations:
     * Might decide make a special case for non-alphabetical symbols.
     * It's possible the user might be helping someone with math homework and will use
     * a lot of + - * / etc.
    ****************************************************************************************/
    public class CalibrationAnalyzer
    {
        public class ErrorStatistics
        {
            private int m_number_of_deletes_pressed = 0;
            private int m_number_of_spelling_errors = 0;
            private int m_number_of_keys_pressed = 0;

            public ErrorStatistics()
            {

            }

            public int NumberOfDeletesPressed
            {
                get
                {
                    return m_number_of_deletes_pressed;
                }
            }

            public int NumberOfSpellingErrors
            {
                get
                {
                    return m_number_of_spelling_errors;
                }
            }

            public int NumberOfKeysPressed
            {
                get
                {
                    return m_number_of_keys_pressed;
                }
            }

            public void IncrementNumberOfDeletesPressed()
            {
                if (m_number_of_deletes_pressed < int.MaxValue)
                    m_number_of_deletes_pressed++;
            }

            public void IncrementNumberOfSpellingErrors()
            {
                if (m_number_of_spelling_errors < int.MaxValue)
                    m_number_of_spelling_errors++;
            }

            public void IncrementNumberOfKeysPressed()
            {
                if (m_number_of_keys_pressed < int.MaxValue)
                    m_number_of_keys_pressed++;
            }

            public void ResetStatistics()
            {
                m_number_of_deletes_pressed = 0;
                m_number_of_spelling_errors = 0;
                m_number_of_keys_pressed = 0;
            }
        }

        // Range: [0, 10]
        private int m_rating = 10;
        private EnglishWordBank m_ewb = new EnglishWordBank();
        private string m_previous_text = "";
        private ErrorStatistics m_es = new ErrorStatistics();

        public CalibrationAnalyzer()
        {

        }

        public ErrorStatistics CalibrationAnalyzerErrorStatistics
        {
            get
            {
                return m_es;
            }
        }

        public int Rating
        {
            get
            {
                return m_rating;
            }
        }

        public bool IsCalibratedProperly()
        {
            if (m_rating >= 8)
                return true;

            return false;
        }

        /****************************************************************************************
         * Make sure you call this function after every time you re-calibrate. Calling
         * this function will reset the rating back to 10. 
        ****************************************************************************************/
        public void ResetRating()
        {
            m_es.ResetStatistics();
            m_rating = 10;
        }

        /****************************************************************************************
         * Make sure you call this function after every time you hit the send button or the
         * clear button.
        ****************************************************************************************/
        public void OnClear()
        {
            m_es.ResetStatistics();
        }

        /****************************************************************************************
         * Make sure you call this function every time you hit the delete buttom.
        ****************************************************************************************/
        public void OnDelete()
        {
            m_es.IncrementNumberOfDeletesPressed();
        }

        private void IncrementRating()
        {
            if (m_rating < 10)
                m_rating++;
        }

        private void DecrementRating()
        {
            if (m_rating > 1)
                m_rating--;
        }

        // This function runs an algorithm to determine the current calibration rating.
        private void CalculateRating(string text_contents, int num_of_predicted_words)
        {
            // If the word we're evaluating is non-empty, non-null, and doesn't
            // exist in the english dictionary
            if (0 == num_of_predicted_words)
            {
                m_es.IncrementNumberOfSpellingErrors();
                DecrementRating();
            }
            // Otherwise 
            else
            {
                // If we have only made at most 2 errors in a row so far,
                // and we have a "good" word now, we can add some rating
                // back and assume that it was just human-error. 
                // If the user made more than 2 errors and haven't corrected
                // those errors, then we can assume the calibration is bad. 
                if (m_rating >= 8)
                {
                    IncrementRating();
                }
            }
        }

        /****************************************************************************************
         * This function has 2 return types. The standard return
         * Returns a bool indicating whether the system is calibrated or not.
         * The second argument parameter will be a reference output variable containing the
         * list of predicted words based on the "right-most" word within the text contents
         * passed to the first argument parameter. (text contents is also non-case sensitive,
         * so don't worry about capital letters BUT, the list of predicted words returned 
         * will all be lower-case. 
         * 
         * EXAMPLE: After you call 
         * bool rating = UpdateAnalyzer("Hello Wor", ref string_list),
         * string_list will contain a list of words; something like this:
         * [ "word", "world", "worm", "worry" ]
         * "rating" will be true if there are no calibration problems and the user is 
         * experienced in eye-typing. 
         * 
         * A rating of 8 to 10 means it's properly calibrated.
         * A rating of 7 to 1 means it's BADLY calibrated.
         * You can retrieve these ratings via the "Rating" property.
         * 
         * Call this function EVERY time you update the UI's main text contents and send
         * the updated text contents to this function. 
        ****************************************************************************************/
        public bool UpdateAnalyzer(string text_contents, ref List<string> predicted_words_list_output)
        {
            string[] text_pieces = text_contents.Split(' ');
            string last_word = text_pieces[text_pieces.Length - 1];

            // If we have an empty or null string, just return the current rating
            if (null == last_word || "" == last_word)
            {
                predicted_words_list_output = new List<string>();

                m_previous_text = text_contents;
                m_es.IncrementNumberOfKeysPressed();
                return IsCalibratedProperly();
            }

            predicted_words_list_output = m_ewb.GetWordPossibilities(last_word);
            CalculateRating(text_contents, predicted_words_list_output.Count);

            m_previous_text = text_contents;
            m_es.IncrementNumberOfKeysPressed();
            return IsCalibratedProperly();
        }
    }
}


