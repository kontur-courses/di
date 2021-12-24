using System;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using ResultProject;

namespace TagsCloudVisualization.Readers
{
    internal class DocxReader : IFileReader
    {
        public TextFormat Format => TextFormat.Docx;

        public Result<string> ReadFile(string filePath)
        {
            return Result.Of(() =>
            {
                using var wordDocument = WordprocessingDocument.Open(filePath, false);
                return GetPlainText(wordDocument.MainDocumentPart?.Document.Body ?? throw new FormatException(filePath));
            }, $"Can't read {filePath} for some reason");
        }
        
        private static string GetPlainText(OpenXmlElement element) 
        {
            var text = new StringBuilder(); 
            foreach (OpenXmlElement section in element.Elements()) 
            {              
                switch (section.LocalName) 
                { 
                    // Text 
                    case "t":  
                        text.Append(section.InnerText); 
                        break; 
                    case "cr":                          // Carriage return 
                    case "br":                          // Page break 
                        text.Append(Environment.NewLine); 
                        break; 
                    // Tab 
                    case "tab": 
                        text.Append("\t"); 
                        break; 
                    // Paragraph 
                    case "p": 
                        text.Append(GetPlainText(section)); 
                        text.AppendLine(Environment.NewLine); 
                        break; 
                    default: 
                        text.Append(GetPlainText(section)); 
                        break; 
                } 
            } 
            return text.ToString();
        }
    }
}