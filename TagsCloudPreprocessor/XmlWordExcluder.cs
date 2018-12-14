using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TagsCloudPreprocessor
{
    public class XmlWordExcluder : IWordExcluder
    {
        public XmlWordExcluder()
        {
            filename = "russianWords.xml";
        }

        private readonly XmlSerializer hashSetSerializer = new XmlSerializer(typeof(HashSet<string>));
        private readonly string filename;

        public HashSet<string> GetExcludedWords()
        {
            return ReadForbiddenWords();
        }

        public void SetExcludedWord(string word)
        {
            var forbiddenWords = ReadForbiddenWords();
            forbiddenWords.Add(word);
            WriteForbiddenWords(forbiddenWords);
        }

        private HashSet<string> ReadForbiddenWords()
        {
            using (var file = new FileStream(filename, FileMode.OpenOrCreate))
            {
                return (HashSet<string>)hashSetSerializer.Deserialize(file);   
            }
        }

        private void WriteForbiddenWords(HashSet<string> forbiddenWords)
        {
            using (var file = new FileStream(filename, FileMode.Create))
            {
                hashSetSerializer.Serialize(file, forbiddenWords);
            }
        }
    }
}
