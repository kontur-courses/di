using System.Collections.Generic;
using MicrosoftWord = Microsoft.Office.Interop.Word;

namespace TagsCloud.App
{
    public class DocFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"doc", "docx"};

        public override string[] ReadLines(string fileName)
        {
            CheckForExceptions(fileName);
            var app = new MicrosoftWord.Application();
            var objFileName = (object) fileName;
            app.Documents.Open(ref objFileName);
            var doc = app.ActiveDocument;
            if (doc.Paragraphs.Count == 1 && doc.Paragraphs.First.Range.Text == "\r")
            {
                app.Quit();
                return new string[0];
            }

            var words = new string[doc.Paragraphs.Count];
            for (var i = 1; i <= words.Length; i++)
                words[i - 1] = doc.Paragraphs[i].Range.Text.Trim('\r');
            app.Quit();
            return words;
        }
    }
}