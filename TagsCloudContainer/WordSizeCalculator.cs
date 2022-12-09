using System.Drawing;

namespace TagsCloudContainer
{
    public class WordSizeCalculator : IWordSizeCalculator
    {
        public Dictionary<string, Font> CalculateSize(Dictionary<string, int> input, IMyConfiguration configuration)
        {
            var result = new Dictionary<string, Font>(input.Count);
            foreach (var pair in input)
                result.Add(pair.Key, new Font(configuration.Font, configuration.FontSize + pair.Value));

            return result;
        }
    }

    public interface IWordSizeCalculator
    {
        public Dictionary<string, Font> CalculateSize(Dictionary<string, int> input, IMyConfiguration configuration);
    }
}