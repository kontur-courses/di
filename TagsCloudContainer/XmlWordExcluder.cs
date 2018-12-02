using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TagsCloudContainer
{
    public class XmlWordExcluder : IWordExcluder
    {
        public XmlWordExcluder()
        {
            file = new FileStream(filename, FileMode.OpenOrCreate);
        }

        private readonly string filename = Environment.CurrentDirectory + "\\..\\..\\Resources\\ExcludedWords\\" + "russianWords.xml";
        private readonly XmlSerializer hashSetSerializer = new XmlSerializer(typeof(HashSet<string>));
        private Stream file; 

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
            return (HashSet<string>)hashSetSerializer.Deserialize(file);
        }

        private void WriteForbiddenWords(HashSet<string> forbiddenWords)
        {
            hashSetSerializer.Serialize(file, forbiddenWords);
        }
    }
}
