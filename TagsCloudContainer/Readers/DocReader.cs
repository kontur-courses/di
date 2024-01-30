using Spire.Doc;
using Spire.Doc.Documents;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Readers
{
    public class DocReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            try
            {
                var document = new Document();
                document.LoadFromFile(filePath);

                return document.Sections
                    .Cast<Section>()
                    .SelectMany(section => section.Paragraphs.Cast<Paragraph>())
                    .SelectMany(paragraph => paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading .doc file: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }
    }
}
