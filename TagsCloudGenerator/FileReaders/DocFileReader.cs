using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Word;


namespace TagsCloudGenerator.FileReaders
{
    public class DocFileReader : IFileReader
    {
        public Dictionary<string, int> ReadWords(string path)
        {
            var application = new Application();
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            var document = application.Documents.Open(newPath);
            var words = new List<string>();

            for (var i = 0; i < document.Paragraphs.Count; i++)
            {
                var text = document.Paragraphs[i + 1].Range.Text;
                words.Add(text.Remove(text.Length - 1));
            }

            application.Quit();

            return WordToCountConverter.GetWordToCount(words);
        }
    }
}