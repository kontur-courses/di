using System;
using System.Collections.Generic;

namespace TagsCloudContainer.WordProcessing
{
    public class ConsoleReader : IWordProvider
    {
        public IEnumerable<string> GetWords()
        {
            var words = new HashSet<string>();
            while (true)
            {
                var word = Console.ReadLine();
                if (word == "")
                    break;
                words.Add(word);
            }

            return words;
        }
    }
}