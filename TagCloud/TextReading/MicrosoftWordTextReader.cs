using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;


namespace TagCloud.TextReading
{
    public class MicrosoftWordTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(FileInfo file)
        {
            using (var wordDocument = WordprocessingDocument.Open(file.FullName, false))
            {
                var body = wordDocument.MainDocumentPart.Document.Body;
                foreach (var element in body.ChildElements)
                {
                    var text = element.InnerText;
                    if (text != "")
                        yield return text;
                }
            }
        }

        public string Extension => ".docx";
    }
}
