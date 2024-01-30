using System.Drawing;

namespace TagsCloudContainer
{
    public class ColorMapper
    {
        public static Dictionary<int, Color> MapColors(IEnumerable<int> numbers, IList<Color> colors)
        {
            var distinctNumbers = numbers.Distinct().OrderByDescending(x => x);
            var colorMapping = new Dictionary<int, Color>();

            if (!distinctNumbers.Any() || !colors.Any())
            {
                return colorMapping;
            }

            int partitionSize = distinctNumbers.Count() / colors.Count;

            int i = 0;
            int j = 0;

            foreach (var number in distinctNumbers)
            {
                colorMapping[number] = colors[j];
                if (i >= partitionSize)
                {
                    i = 0;
                    j = Math.Min(colors.Count - 1, j + 1);
                }
                i++;
            }

            return colorMapping;
        }
    }
}
