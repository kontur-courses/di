using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextRiders
{
    public class FileFileTextRider : IFileTextRider
    {
        private TextRiderConfig textRiderConfig;
        
        public FileFileTextRider(TextRiderConfig textRiderConfig)
        {
            this.textRiderConfig = textRiderConfig;
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