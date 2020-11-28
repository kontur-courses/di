using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public static class SizesCreator
    {
        public static Size[] CreateSizesArray(IEnumerable<string> words)
        {
            var frequencyDict = GetFrequencyDictionary(words);
            throw new NotImplementedException();
        }

        private static Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            throw new NotImplementedException();
        }
    }
}