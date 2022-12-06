using System;
using System.IO;

namespace TagsCloudVisualization
{
    public class FileGenerator
    {
        private readonly Random random = new Random();
        private readonly string[] words = new[]
        {
            "Apple", "Microsoft", "JetBrains", "Wiki", "Windows", "CopyPaste", "Linux", "Tim", "Bill", "Tsvetkov", 
            "Andronov", "Mishurin", "Konovalov", "Davletbaev", "Fruit", "Jenga", "Kubernetes", "Samsung", "Tinkoff", "SKB",
            "Mother", "Alex", "Max", "SQWOZBAB", "Command", "Lion", "Discord", "Teams", "Son", "Pen", "Java", "C#", "Python",
            "We", "They", "He", "Him", "All",
            "Or", "And", "A", "An", "In", "Out"
        };

        public void Generate(string fileName, int amountLines)
        {
            using var writer = new StreamWriter(fileName);
            
            for (var i = 0; i < amountLines; i++)
                writer.WriteLine(words[random.Next(0, words.Length)]);
        }
    }
}