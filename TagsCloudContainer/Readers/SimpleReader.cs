using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace TagsCloudContainer.Readers
{
    public class SimpleReader : IReader
    {
        private string path { get; }
        private Dictionary<string, Func<string, string[]>> readers;

        public SimpleReader(string path)
        {
            this.path = path;

            readers = new Dictionary<string, Func<string, string[]>>
            {
                { "txt", pathToText => ReadOther(pathToText) },
                { "doc", pathToText => ReadDoc(pathToText) },
                { "docx", pathToText => ReadDoc(pathToText) }
            };
        }

        public string[] ReadAllLines()
        {
            var splitPath = path.Split('.');
            return readers[splitPath[splitPath.Length - 1]](path);
        }

        private string[] ReadOther(string path)
        {
            var stream = new StreamReader(path);
            var stringSeparators = new[] { "\r\n" };
            return stream.ReadToEnd().Split(stringSeparators, StringSplitOptions.None);
        }

        private string[] ReadDoc(string path)
        {
            var text = new List<string>();
            Word.Application app = new Word.Application();
            app.Documents.Open(path);
            Word.Document doc = app.ActiveDocument;
            for (int i = 1; i < doc.Paragraphs.Count; i++)
            {
                var word = doc.Paragraphs[i].Range.Text;
                text.Add(word.Substring(0, word.Length - 1));
            }
            doc.Close();
            app.Quit();
            return text.ToArray();
        }
    }
}
