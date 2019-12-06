using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer.Parsing
{
    public class TxtParser : IFileParser
    {
        public IEnumerable<string> ParseFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}