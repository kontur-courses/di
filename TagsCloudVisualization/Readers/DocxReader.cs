using System;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace TagsCloudVisualization.Readers
{
    public class DocxReader : IFileReader
    {
        public TextFormat Format => TextFormat.Docx;

        public string ReadFile(string filePath)
        {
            using var wordDocument = WordprocessingDocument.Open(filePath, false);
            return GetPlainText(wordDocument.MainDocumentPart?.Document.Body ?? throw new FormatException(filePath));
        }
        
        private static string GetPlainText(OpenXmlElement element) 
        {
            StringBuilder text = new(); 
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