using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace TagCloud
{
    public class DocxFileReader : IFileReader
    {
        public string ReadAllText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} doesn't exist");

            var sb = new StringBuilder();

            using (WordprocessingDocument wordDocument =
                    WordprocessingDocument.Open(filePath, false))
            {
                var paragraphs = wordDocument.MainDocumentPart.RootElement.Descendants<Paragraph>();

                foreach (var paragraph in paragraphs)
                    sb.AppendLine(paragraph.InnerText);

                wordDocument.Close();

                return sb.ToString().Trim();
            }
        }
    }
}