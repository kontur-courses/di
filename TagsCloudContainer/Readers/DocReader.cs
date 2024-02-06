using Spire.Doc;
using Spire.Doc.Documents;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.Readers
{
    public class DocReader : IFileReader
    {
        public Result<IEnumerable<string>> ReadWords(string filePath)
        {
            try
            {
                var document = new Document();
                document.LoadFromFile(filePath);

                var words = document.Sections
                    .Cast<Section>()
                    .SelectMany(section => section.Paragraphs.Cast<Paragraph>())
                    .SelectMany(paragraph => paragraph.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries));

                return Result<IEnumerable<string>>.Success(words);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<string>>.Failure($"Error reading .doc file: {ex.Message}");
            }
        }
    }
}
