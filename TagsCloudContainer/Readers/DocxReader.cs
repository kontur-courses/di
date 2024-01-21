using NPOI.XWPF.UserModel;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Readers
{
    public class DocxReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            var words = new List<string>();

            try
            {
                using (var doc = new XWPFDocument(File.OpenRead(filePath)))
                {
                    foreach (var paragraph in doc.Paragraphs)
                    {
                        words.AddRange(paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading .docx file: {ex.Message}");
            }

            return words;
        }
    }
}
