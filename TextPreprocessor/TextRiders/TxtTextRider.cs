using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextPreprocessor.Core;

namespace TextPreprocessor.TextRiders
{
    public class TxtTextRider : IFileTextRider
    {
        private TextRiderConfig textRiderConfig;
        
        public TxtTextRider(TextRiderConfig textRiderConfig)
        {
            this.textRiderConfig = textRiderConfig;
        }
        
        public IEnumerable<Tag> GetTags()
        {
            return GetFileContent(textRiderConfig.FilePath)
                .Split(textRiderConfig.WordsDelimiters)
                .Select(str => textRiderConfig.GetCorrectWordFormat(str))
                .Where(str => !textRiderConfig.IsSkipWord(str))
                .Where(str => str != "")
                .Select(str => new Tag(str));
        }

        private string GetFileContent(string filePath)
        {
            string text;
            
            using (StreamReader sr = new StreamReader(filePath))
            {
                text = sr.ReadToEnd();
            }

            return text;
        }
    }
}