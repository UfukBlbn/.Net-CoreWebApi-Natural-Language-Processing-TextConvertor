using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NLP_WebApi.Extensions
{
    public class RegexNumberSplitter
    {
        public static List<string> NumberSplitter(string input)
        {

            List<string> numberList = new List<string>();

            // Split on one or more non-digit characters.
            string[] numbers = Regex.Split(input, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    numberList.Add(i.ToString());
                }
            }

            return numberList;
        }
    }
}
