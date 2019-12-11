using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer.TextParsing.FileWordsParsers
{
    public class TxtWordParser : IFileWordsParser
    {
        public IEnumerable<string> ParseFrom(string path)
        {
            using (var file = new StreamReader(path, Encoding.Default))
            {
                var line = "";
                while ((line = file.ReadLine()) != null)
                    yield return line;
            }
        }
    }
}