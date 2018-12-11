using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    static class IEnumerableExtensions
    {
        public static string[] RemoveBoring(this IEnumerable<string> words)
        {
            var boringWords = new List<string>();
            foreach (var file in Directory.EnumerateFiles("BoringWords"))
            {
                boringWords.AddRange(File.ReadAllLines(file));
            }
            
            return words
                .Where(w => w.Length > 2)
                .Except(boringWords)
                .ToArray();
        }
    }
}
