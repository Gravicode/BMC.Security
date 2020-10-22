using System;
using System.Text;

namespace BMC.Security.Tools
{
    public  class NumberGen
    {
        static Random rnd = new Random(Environment.TickCount);
        public static string GenerateNumber(int length=10)
        {
            var NumStr = "";
            for(int i = 0; i < length; i++)
            {
                NumStr += rnd.Next(0, 10);
            }
            return NumStr;
        }
    }

    public class StringGen
    {
        static Random _random;
        public static string RandomString(int size, bool lowerCase = false)
        {
            if (_random == null) _random = new Random();
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}
