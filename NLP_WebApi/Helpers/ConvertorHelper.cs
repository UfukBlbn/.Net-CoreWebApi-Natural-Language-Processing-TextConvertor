using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Helpers
{
    public class ConvertorHelper
    {
        public static string ConvertingTextToString(string text)
        {
            return Convert.ToString(text);

        }
        public static string Capitalization(string text)
        {
            if (text != "")
            {
                var lowerCase = Convert.ToString(text).ToLower();
                var firstLetter = lowerCase[0];
                return IsDigitsOnly(Convert.ToString(firstLetter)) == true ? text : char.ToUpper(lowerCase[0]).ToString() + lowerCase.Substring(1);
            }
            else
            {
                return text;
            }


        }
        public static string UserTextFormatter(string userText, string word, string number)
        {
            if (userText != "")
            {
                var formattedText = userText.ToLower().ToString().Replace(word, number);
                return formattedText;
            }
            else
            {
                return userText;
            }

        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
