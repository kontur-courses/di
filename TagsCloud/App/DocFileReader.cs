using System.Collections.Generic;
using MicrosoftWord = Microsoft.Office.Interop.Word;

namespace TagsCloud.App
{
    public class DocFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"doc", "docx"};

        public override IEnumerable<string> ReadLines(string fileName)
        {
            CheckForExceptions(fileName);
            var app = new MicrosoftWord.Application();
            var objFileName = (object) fileName;
            app.Documents.Open(ref objFileName);
            var doc = app.ActiveDocument;
            if (doc.Paragraphs.Count == 1 && doc.Paragraphs.First.Range.Text == "\r")
            {
                app.Quit();
                yield break;
            }

            for (var i = 1; i <= doc.Paragraphs.Count; i++)
                foreach (var word in GetWords(doc.Paragraphs[i].Range.Text.Trim('\r')))
                    yield return word;
            app.Quit();
        }
    }
}