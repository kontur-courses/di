using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TagCloud.Core.TextWorking.WordsReading.WordsReadersForFiles
{
    public class XmlWordsReader : IWordsReaderForFile
    {
        public string ReadingFileExtension { get; }
        private readonly XmlSerializer serializer;

        public XmlWordsReader()
        {
            ReadingFileExtension = ".xml";
            serializer = new XmlSerializer(typeof(string[]));
        }

        public IEnumerable<string> ReadFrom(string path)
        {
            using (var r = new StreamReader(path))
            {
                return (string[])serializer.Deserialize(r);
            }
        }
    }
}