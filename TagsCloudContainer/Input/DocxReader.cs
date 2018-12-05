using System.IO;
using System.Text;
using NPOI.XWPF.UserModel;

namespace TagsCloudContainer.Input
{
    public class DocxReader : IFileReader
    {
        public string Read(string filename)
        {
            var result = new StringBuilder();

            XWPFDocument doc;
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                doc = new XWPFDocument(file);
            }

            foreach (var paragraph in doc.Paragraphs)
            {
                var text = paragraph.ParagraphText;
                result.Append(text);
            }

            return result.ToString();
        }
    }
}