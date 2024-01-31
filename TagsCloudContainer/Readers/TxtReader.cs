using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Readers
{
    public class TxtReader : IFileReader
    {
        public IEnumerable<string> ReadWords(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                var words = lines.SelectMany(line => line.Split(' '));

                var nonEmptyWords = words.Where(word => !string.IsNullOrEmpty(word));

                return nonEmptyWords;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }
    }
}
