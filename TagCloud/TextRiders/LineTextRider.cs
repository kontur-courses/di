using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagCloud
{
    public class LineTextRider : ITextRider
    {
        private readonly ITextRiderConfig textRiderConfig;
        public LineTextRider(ITextRiderConfig config)
        {
            textRiderConfig = config;
        }
        
        public IEnumerable<Tag> GetWords()
        {
            return GetTextLines(textRiderConfig.FilePath)
                .Where(str => textRiderConfig.WordFilter(str))
                .Select(str => new Tag(textRiderConfig.WordFormat(str)));
        }

        private IEnumerable<string> GetTextLines(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                    yield return sr.ReadLine();
            }
        }
    }
}