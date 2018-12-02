using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TagsCloudContainer
{
    public class XmlWordExcluder : IWordExcluder
    {
        public XmlWordExcluder(string pathToSolutionDirectory)
        {
            var filename = pathToSolutionDirectory + "\\Resources\\ExcludedWords\\" + "russianWords.xml";
            file = new FileStream(filename, FileMode.OpenOrCreate);
        }

        private readonly XmlSerializer hashSetSerializer = new XmlSerializer(typeof(HashSet<string>));
        private readonly Stream file;

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
