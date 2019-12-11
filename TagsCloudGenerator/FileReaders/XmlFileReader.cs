using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TagsCloudGenerator.FileReaders
{
    public class XmlFileReader : IFileReader
    {
        public Dictionary<string, int> ReadWords(string path)
        {
            var doc = XDocument.Load(path);

            if (doc.Root == null)
                throw new ArgumentException("invalid file content format");

            if (doc.Root.Name.LocalName != "words")
                throw new InvalidOperationException("no words root tag");

            var entries = doc.Root.Descendants("entry");
            var wordToCount = new Dictionary<string, int>();

            foreach (var entry in entries)
            {
                var word = entry.Attribute("value")?.Value;
                var count = entry.Attribute("count")?.Value;

                if (word == null || count == null)
                    continue;

                if (!wordToCount.ContainsKey(word))
                    wordToCount.Add(word, 0);

                wordToCount[word] += Convert.ToInt32(count);
            }

            return wordToCount;
        }
    }
}