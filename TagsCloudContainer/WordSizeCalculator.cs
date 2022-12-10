using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class WordSizeCalculator : IWordSizeCalculator
    {
        public Dictionary<string, Font> CalculateSize(Dictionary<string, int> input, ICustomOptions options)
        {
            var result = new Dictionary<string, Font>(input.Count);
            foreach (var pair in input)
                result.Add(pair.Key, new Font(options.Font, options.FontSize + pair.Value));

            return result;
        }
    }
}