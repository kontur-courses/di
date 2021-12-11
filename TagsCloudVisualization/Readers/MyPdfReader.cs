using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagsCloudVisualization.Readers
{
    public class MyPdfReader : IFileReader
    {
        public TextFormat Format => TextFormat.Pdf;

        public string ReadFile(string filePath)
        {
            StringBuilder text = new();  
            using (var reader = new PdfReader(filePath))  
            {  
                for (var i = 1; i <= reader.NumberOfPages; i++)  
                {  
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));  
                }  
            }  
   
            return text.ToString();  
        }
    }
}