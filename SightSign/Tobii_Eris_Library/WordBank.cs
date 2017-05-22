using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tobii_Eris_Library
{
    /*************************************IMPORANT NOTE**************************************
     * The AddWord(s) and RemoveWord(s) functions are DEPENDENT on the FindWord(s) 
     * function and the FindWord(s) function is DEPENDENT on the VIRTUAL function
     * WordCompare(s1, s2). 
     * 
     * Call order: 
     * AddWord(s) / RemoveWord(s) ---> FindWordAndIndex(s) / FindWord(s) / GetWordPossibilities(s) ---> WordCompare(s1, s2)
    ****************************************************************************************/

    /*************************************GENERAL INFO***************************************
     * Although looking up words through a HashSet is faster than a Binary-find through
     * a sorted list, we need to use a list so that we have information on the most-similar
     * word based on the given prefix as well as its neighbors, which share the same 
     * prefix, so that we can properly implement a word-prediction system. 
    ****************************************************************************************/
    public abstract class WordBank
    {
        protected List<string> m_main_word_list = new List<string>();

        // This list is for custom words. This feature will be implemented at a later time. 
        protected List<string> m_custom_word_list = new List<string>();

        public WordBank(string word_list_file_path, bool is_already_sorted = false)
        {
            string current_word = "";
            //If you have error, right click on "MainUI" then -> Properties ->target framework -> .Net Framework ** (whatever the biggest nummber is)
            //Also, make sure that the "MainUI" is the startup project

            if (!GeneralTools.CheckFileExists(word_list_file_path))
                throw new FileNotFoundException("File \"" + word_list_file_path + "\" not found.");

            StreamReader reader = new StreamReader(word_list_file_path);

            while (!reader.EndOfStream)
            {
                current_word = reader.ReadLine();
                current_word = current_word.ToLower();
                if (is_already_sorted)
                    m_main_word_list.Add(current_word);
                else
                    AddWord(m_main_word_list, current_word);
            }

            reader.Close();
        }

        public int BankSize()
        {
            return m_main_word_list.Count + m_custom_word_list.Count;
        }

        /****************************************************************************************
         * This function runs on a binary-search algorithm. 
         * 
         * This function finds the given word in the list and returns true if found and false
         * otherwise. If the word is found, its index will be outputted to the reference
         * argument "index_found_at". Otherwise if not found, the index of where the word 
         * "would be at" if it were found will be outputted to index_found_at, for the purpose
         * of helping the FindWord(s) function.  
        ****************************************************************************************/
        protected bool FindWordAndIndex(List<string> list_to_search, string word_to_find, ref int output_index)
        {
            // First, we will do some preparation
            word_to_find = word_to_find.ToLower();
            output_index = 0;
            int left_i = 0, right_i = BankSize() - 1;

            // Now, we will Binary-search
            while (left_i <= right_i)
            {
                int current_index = (left_i + right_i) / 2;

                if (GeneralTools.ComparisonResult.LessThan == WordCompare(list_to_search[current_index], word_to_find))
                {
                    left_i = current_index + 1;
                    output_index = left_i;
                    continue;
                }
                else if (GeneralTools.ComparisonResult.GreaterThan == WordCompare(list_to_search[current_index], word_to_find))
                {
                    right_i = current_index - 1;
                    output_index = current_index;
                    continue;
                }
                else if (GeneralTools.ComparisonResult.EqualTo == WordCompare(list_to_search[current_index], word_to_find))
                {
                    output_index = current_index;
                    return true;
                }
            }

            return false;
        }

        // Currently not used, may be implemented later
        public bool FindWord(string word_to_find)
        {
            throw new NotImplementedException();
        }

        /****************************************************************************************
         * Returns a list of words that start with the given prefix substring if there are
         * any. If you only want to retrieve x amount of words but there are more than x
         * number of words that meet the specified condition, you can specify x for the
         * second parameter argument. 
        ****************************************************************************************/
        public List<string> GetWordPossibilities(string prefix_substring, int returned_list_size_cap = int.MaxValue)
        {
            prefix_substring = prefix_substring.ToLower();
            List<string> possibilities = new List<string>();
            int start_index = 0;

            // We will use start index to refer to the closest matched word found in the list. 
            FindWordAndIndex(m_main_word_list, prefix_substring, ref start_index);

            for (int i = start_index, j = 0; i < BankSize() && j < returned_list_size_cap; i++, j++)
            {
                // If what we currently have is no longer a prefix substring of what's available,
                // return what we have.
                if (!m_main_word_list[i].StartsWith(prefix_substring))
                    return possibilities;

                possibilities.Add(m_main_word_list[i]);
            }

            // Below, we will implement the search-through of the custom word list at a later time.


            return possibilities;
        }

        /****************************************************************************************
         * Used for both the main list and the custom list. We don't want to alter the main
         * list anywhere else but the child class' constructor, therefore we keep this 
         * version of the function protected. 
        ****************************************************************************************/
        protected bool AddWord(List<string> list_to_add_to, string word_to_add)
        {
            word_to_add = word_to_add.ToLower();
            int index = -1;

            if (!FindWordAndIndex(list_to_add_to, word_to_add, ref index))
            {
                if (BankSize() == index)
                    m_main_word_list.Add(word_to_add);
                else
                    m_main_word_list.Insert(index, word_to_add);

                return true;
            }

            return false;
        }

        // Used for custom words and is available to the public, will be implemented later
        public bool AddWord(string word_to_add)
        {
            throw new NotImplementedException();
        }

        // Used for custom words, will be implemented later
        public bool RemoveWord(string word_to_add)
        {
            throw new NotImplementedException();
        }

        /****************************************************************************************
         * This function is the comparison function used by the FindWord(s), AddWord(s), and
         * RemoveWord(s) methods.
         * The default method uses the standard string.Compare(s1, s2) function. If it does not
         * behave the way you want it to, you may override it. 
        ****************************************************************************************/
        protected virtual GeneralTools.ComparisonResult WordCompare(string left_param, string right_param)
        {
            if (string.Compare(left_param, right_param) < 0)
                return GeneralTools.ComparisonResult.LessThan;
            else if (string.Compare(left_param, right_param) > 0)
                return GeneralTools.ComparisonResult.GreaterThan;
            else
                return GeneralTools.ComparisonResult.EqualTo;
        }

        // For Debug
        public void PrintList()
        {
            foreach (string word in m_main_word_list)
            {
                Console.WriteLine(word);
            }
        }
    }
}
