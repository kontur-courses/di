using System;
using System.IO;

namespace TagsCloudVisualization.Tests
{
    public static class FileGenerator
    {
        public static void Generate(string fileName, string[] words, int amountLines)
        {
            var random = new Random();
            
            using var writer = new StreamWriter(fileName);

            for (var i = 0; i < amountLines; i++)
                writer.WriteLine(words[random.Next(0, words.Length)]);
        }
    }
}