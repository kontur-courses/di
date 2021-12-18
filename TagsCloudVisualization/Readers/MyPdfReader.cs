using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ResultProject;

namespace TagsCloudVisualization.Readers
{
    public class MyPdfReader : IFileReader
    {
        public TextFormat Format => TextFormat.Pdf;

        public Result<string> ReadFile(string filePath)
        {
            return Result.Of(() =>
            {
                var text = new StringBuilder();  
                using (var reader = new PdfReader(filePath))  
                {  
                    for (var i = 1; i <= reader.NumberOfPages; i++)  
                    {  
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));  
                    }  
                }  
   
                return text.ToString();  
            }, $"Can't read {filePath} for some reason");
        }
    }
}