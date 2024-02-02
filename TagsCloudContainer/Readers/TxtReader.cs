using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Utility;

namespace TagsCloudContainer.Readers
{
    public class TxtReader : IFileReader
    {
        public Result<IEnumerable<string>> ReadWords(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                var words = lines.SelectMany(line => line.Split(' '));

                var nonEmptyWords = words.Where(word => !string.IsNullOrEmpty(word));

                return Result<IEnumerable<string>>.Success(nonEmptyWords);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<string>>.Failure($"Error reading file: {ex.Message}");
            }
        }
    }
}
