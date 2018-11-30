using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TagsCloudContainer
{
    public class SimplePreprocessor : IPreprocessor
    {
        public IEnumerable<string> GetValidWords(IEnumerable<string> words)
        {
            var forbiddenWords = GetForbiddenWords();
            var frequencyDictionary = GetFrequencyDictionary(words.Where(w => !forbiddenWords.Contains(w)));
            var validWords = frequencyDictionary
                .OrderBy(pair => pair.Value)
                .Reverse()
                .Select(pair => pair.Key);

            return validWords;
        }

        private Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDictionary = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!frequencyDictionary.ContainsKey(word))
                    frequencyDictionary[word] = 1;
                frequencyDictionary[word]++;
            }

            return frequencyDictionary;
        }

        private HashSet<string> GetForbiddenWords()
        {
            var filename = Environment.CurrentDirectory + "\\..\\..\\"+ "words.xml";
            var file = new FileStream(filename, FileMode.Open);
            return (HashSet<string>)new XmlSerializer(typeof(HashSet<string>)).Deserialize(file);
        }
    }
}
