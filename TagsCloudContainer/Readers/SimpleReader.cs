using System.IO;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace TagsCloudContainer.Readers
{
    class SimpleReader : IReader
    {
        private string path { get; }

        public SimpleReader(string path)
        {
            this.path = path;
        }

        public string[] ReadAllLines()
        {
            var splitPath = path.Split('.');
            if (new[] {"doc", "docx" }.Contains(splitPath[splitPath.Length - 1]))
            {
                return ReadDoc(path);
            }
            return ReadOther(path);
        }

        private string[] ReadOther(string path)
        {
            var stream = new StreamReader(path);
            return stream.ReadToEnd().Split('\n');
        }

        private string[] ReadDoc(string path)
        {
            Word.Application app = new Word.Application();
            app.Documents.Open(path);
            Word.Document doc = app.ActiveDocument;
            var words = new string[doc.Paragraphs.Count - 1];
            for (int i = 1; i < doc.Paragraphs.Count; i++)
            {
                var text = doc.Paragraphs[i].Range.Text;
                words[i - 1] = text.Substring(0, text.Length - 1);
            }
            doc.Close();
            app.Quit();
            return words;
        }
    }
}
