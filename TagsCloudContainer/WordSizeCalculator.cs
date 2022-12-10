using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class WordSizeCalculator : IWordSizeCalculator
    {
        public Dictionary<string, Font> CalculateSize(Dictionary<string, int> input, ICustomOptions options)
        {
            var result = new Dictionary<string, Font>(input.Count);
            var max = input.First().Value;
            var min = input.Last().Value;

            var fontMax = options.MaxTagSize;
            var fontMin = options.MinTagSize;

            foreach (var pair in input)
            {
                var size = pair.Value == min ? fontMin
                    : (pair.Value / (double)max) * (fontMax - fontMin) + fontMin;
                result.Add(pair.Key, new Font(options.Font, (int)size));
            }

            return result;
        }
    }
}