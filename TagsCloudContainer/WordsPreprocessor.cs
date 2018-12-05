using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordsPreprocessor
    {
        public static Dictionary<string, int> Prepare(string fileName)
        {
            var words = File.ReadAllLines(fileName)
                .Select(word => word.ToLower());

            var dic = words
                .Distinct()
                .ToDictionary(w => w, w => words.Count(s => s == w));

            return dic;
        }

        private static string[] RemoveBoring(string[] words)
        {
            return new string[0];
        }
    }
}
