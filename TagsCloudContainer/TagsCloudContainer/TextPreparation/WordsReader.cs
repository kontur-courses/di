using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.TextPreparation
{
    public class WordsReader : IWordsReader
    {
        public List<string> ReadAllWords(string fileContent)
        {
            var lines = fileContent.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            if (lines.Any(line => (line).Contains(' ')))
            {
                throw new ArgumentException("Each line must contain only one word");
            }

            return lines.Where(line => !string.IsNullOrEmpty(line)).ToList();
        }
    }
}