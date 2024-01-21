using Spire.Doc;
using Spire.Doc.Documents;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Readers
{
    public class DocReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            var words = new List<string>();

            try
            {
                var document = new Document();
                document.LoadFromFile(filePath);

                foreach (Section section in document.Sections)
                {
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        words.AddRange(paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading .doc file: {ex.Message}");
            }

            return words;
        }
    }
}
