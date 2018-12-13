using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TagCloud.Core.WordsParsing.WordsReading
{
    public class XmlWordsReader : IWordsReader
    {
        public Regex AllowedFileExtension { get; }
        private readonly XmlSerializer serializer;

        public XmlWordsReader()
        {
            AllowedFileExtension = new Regex(@"\.xml$", RegexOptions.IgnoreCase);
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