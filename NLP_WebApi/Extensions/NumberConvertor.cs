using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLP_WebApi.Extensions
{
    public static class NumberConvertor
    {

        //converts any number between 0 & INT_MAX (2,147,483,647)
        public static string ConvertNumberToString(int n)
        {
            if (n < 0)
                throw new NotSupportedException("- sayılar kullanılamaz");
            if (n == 0)
                return "sıfır";
            if (n < 10)
                return ConvertDigitToString(n);
            if (n < 20)
                return ConvertTeensToString(n);
            if (n < 100)
                return ConvertHighTensToString(n);
            if (n < 1000)
                return ConvertBigNumberToString(n, (int)1e2, "yüz");
            if (n < 1e6)
                return ConvertBigNumberToString(n, (int)1e3, "bin");
            if (n < 1e9)
                return ConvertBigNumberToString(n, (int)1e6, "milyon");
            //if (n < 1e12)
            return ConvertBigNumberToString(n, (int)1e9, "trilyon");
        }

        private static string ConvertDigitToString(int i)
        {
            switch (i)
            {
                case 0: return "";
                case 1: return "bir";
                case 2: return "iki";
                case 3: return "üç";
                case 4: return "dört";
                case 5: return "beş";
                case 6: return "altı";
                case 7: return "yedi";
                case 8: return "sekiz";
                case 9: return "dokuz";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} bir rakam değil", i));
            }
        }

        //assumes a number between 10 & 19
        private static string ConvertTeensToString(int n)
        {
            switch (n)
            {
                case 10: return "on";
                case 11: return "onbir";
                case 12: return "oniki";
                case 13: return "onüç";
                case 14: return "ondört";
                case 15: return "onbeş";
                case 16: return "onaltı";
                case 17: return "onyedi";
                case 18: return "onsekiz";
                case 19: return "ondokuz";
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} onluk basamakta değil", n));
            }
        }

        //assumes a number between 20 and 99
        private static string ConvertHighTensToString(int n)
        {
            int tensDigit = (int)(Math.Floor((double)n / 10.0));

            string tensStr;
            switch (tensDigit)
            {
                case 2: tensStr = "yirmi"; break;
                case 3: tensStr = "otuz"; break;
                case 4: tensStr = "kırk"; break;
                case 5: tensStr = "elli"; break;
                case 6: tensStr = "altmış"; break;
                case 7: tensStr = "yetmiş"; break;
                case 8: tensStr = "seksen"; break;
                case 9: tensStr = "doksan"; break;
                default:
                    throw new IndexOutOfRangeException(String.Format("{0} 20-99 aralığında değil", n));
            }
            if (n % 10 == 0) return tensStr;
            string onesStr = ConvertDigitToString(n - tensDigit * 10);
            return tensStr + "-" + onesStr;
        }




        private static string ConvertBigNumberToString(int n, int baseNum, string baseNumStr)
        {
            // special case: use commas to separate portions of the number, unless we are in the hundreds
            string separator = (baseNumStr != "yüz") ? ", " : " ";

            // Strategy: translate the first portion of the number, then recursively translate the remaining sections.
            // Step 1: strip off first portion, and convert it to string:
            int bigPart = (int)(Math.Floor((double)n / baseNum));

            if (bigPart == 1)
            {
                string bigPartStr = "yüz";
                // Step 2: check to see whether we're done:
                if (n % baseNum == 0) return bigPartStr;
                // Step 3: concatenate 1st part of string with recursively generated remainder:
                int restOfNumber = n - bigPart * baseNum;
                return bigPartStr + separator + ConvertNumberToString(restOfNumber);
            }
            else
            {
                string bigPartStr = ConvertNumberToString(bigPart) + " " + baseNumStr;
                // Step 2: check to see whether we're done:
                if (n % baseNum == 0) return bigPartStr;
                // Step 3: concatenate 1st part of string with recursively generated remainder:
                int restOfNumber = n - bigPart * baseNum;
                return bigPartStr + separator + ConvertNumberToString(restOfNumber);
            }


        }
    }
}
