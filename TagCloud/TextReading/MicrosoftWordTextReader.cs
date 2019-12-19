using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace TagCloud.TextReading
{
    public class MicrosoftWordTextReader : ITextReader
    {
        public IEnumerable<string> ReadWords(FileInfo file)
        {
            Body body;
            try
            {
                using (var wordDocument = WordprocessingDocument.Open(file.FullName, false))
                {
                    body = wordDocument.MainDocumentPart.Document.Body;
                }
            }
            catch (IOException e)
            {
                throw new IOException($"File {file.FullName} is in use", e);
            }
            foreach (var element in body.ChildElements)
            {
                var text = element.InnerText;
                if (text != "")
                    yield return text;
            }
        }

        public string Extension => ".docx";
    }
}
