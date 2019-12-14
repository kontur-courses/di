using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Word;
using TagsCloudGenerator.Tools;


namespace TagsCloudGenerator.FileReaders
{
    public class DocFileReader : IFileReader
    {
        private readonly IWordsParser parser;

        public DocFileReader(IWordsParser parser)
        {
            this.parser = parser;
        }

        public Dictionary<string, int> ReadWords(string path)
        {
            var words = parser.Parse(ReadFile(path));
            return ParseHelper.GetWordToCount(words);
        }

        private static string ReadFile(string path)
        {
            var application = new Application();
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            var document = application.Documents.Open(newPath);
            var text = new StringBuilder();

            for (var i = 0; i < document.Paragraphs.Count; i++)
            {
                var temp = document.Paragraphs[i + 1].Range.Text.Trim();
                if (temp != string.Empty)
                    text.Append(temp);
            }

            document.Close();
            application.Quit();

            return text.ToString();
        }
    }
}