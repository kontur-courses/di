using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TagsCloudContainer
{
    public class XmlWordExcluder : IWordExcluder
    {
        private readonly string filename = Environment.CurrentDirectory + "\\..\\..\\Resources\\ExcludedWords\\" + "russianWords.xml";

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
            var file = new FileStream(filename, FileMode.Open);
            return (HashSet<string>)new XmlSerializer(typeof(HashSet<string>)).Deserialize(file);
        }

        private void WriteForbiddenWords(HashSet<string> forbiddenWords)
        {
            var file = new FileStream(filename, FileMode.OpenOrCreate);
            new XmlSerializer(typeof(HashSet<string>)).Serialize(file, forbiddenWords);
        }
    }
}
