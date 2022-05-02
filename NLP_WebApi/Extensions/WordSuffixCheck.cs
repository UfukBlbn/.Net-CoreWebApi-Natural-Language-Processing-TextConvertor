using NLP_WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Extensions
{
    public static class WordSuffixCheck
    {
        public static string ContainsAnySuffix(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))

                    return haystack.Replace(needle, needle.PadLeft(3));
            }

            return haystack;
        }
        public static string AddSuffixBack(string seperatedSuffix, params string[] needles)
        {

            foreach (string needle in needles)
            {

                if (seperatedSuffix.Contains(needle))
                {
                    var indexofneedle = seperatedSuffix.IndexOf(needle);
                    string headPartOfWord = seperatedSuffix.Substring(0, indexofneedle - 1);
                    string tailPartOfWord = seperatedSuffix.Substring(indexofneedle + 2, seperatedSuffix.Length - indexofneedle - 2);
                    string updatedTargetWord = headPartOfWord + needle + "" + tailPartOfWord;
                    return ConvertorHelper.Capitalization(updatedTargetWord);
                }

            }

            return seperatedSuffix;


        }
    }
}

