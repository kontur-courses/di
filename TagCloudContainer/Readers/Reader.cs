using Spire.Doc;
using Spire.Doc.Documents;
using System.Text;

namespace TagCloudContainer.Readers
{
    public class Reader : IFileReader
    {
        public string TxtRead(string path)
        {
            return File.ReadAllText(path);
        }
        public string DocRead(string path)
        {
            var stringBuilder = new StringBuilder();
            var document = new Document(path);
            foreach (Section section in document.Sections)
                foreach (Paragraph paragraph in section.Paragraphs)
                    stringBuilder.AppendLine(paragraph.Text);
            return stringBuilder.ToString();
        }
    }
}
