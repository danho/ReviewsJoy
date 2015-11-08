using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewsJoy.HelperMethods
{
    public class TextHelperMethods
    {
        public static string UppercaseFirst(string s)
        {
            if (String.IsNullOrEmpty(s))
                return String.Empty;

            var words = s.Split(' ');

            var output = String.Empty;
            foreach (var word in words)
            {
                output += " " + UpperCaseFirstLetterOfWord(word);
            }
            return output;
        }

        private static string UpperCaseFirstLetterOfWord(string word)
        {
            if (String.IsNullOrEmpty(word))
                return String.Empty;
            return Char.ToUpper(word[0]) + word.ToLower().Substring(1);
        }
    }
}