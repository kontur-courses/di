using System;
using System.IO;

namespace TagsCloudVisualization
{
    public class FileGenerator
    {
        private readonly Random random = new Random();
        private readonly string[] words = new[]
        {
            "Apple", "Microsoft", "JetBrains", "Wiki", "Windows", "CopyPaste", "Linux", "Tim Cook", "Bill Gates",
            "We", "They", "He", "Him", "All",
            "Or", "And", "A", "An", "In", "Out"
        };

        public void Generate(string fileName, int amountWords)
        {
            using var writer = new StreamWriter(fileName);
            
            for (var i = 0; i < amountWords; i++)
                writer.WriteLine(words[random.Next(0, words.Length)]);
        }
    }
}