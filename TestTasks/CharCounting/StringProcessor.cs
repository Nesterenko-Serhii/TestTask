using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTasks.VowelCounting
{
    public class StringProcessor
    {
        public (char symbol, int count)[] GetCharCount(string veryLongString, char[] countedChars)
        {
            var charCount = new Dictionary<char, int>();
            foreach (var c in countedChars)
            {
                charCount[c] = 0;
            }

            foreach (var c in veryLongString)
            {
                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
            }
            
            return countedChars.Select(c => (c, (int) charCount[c])).ToArray();
        }
    }
}
