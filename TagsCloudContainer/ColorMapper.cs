﻿using System.Drawing;

namespace TagsCloudContainer
{
    public class ColorMapper
    {
        public static Dictionary<int, Color> MapColors(IEnumerable<int> numbers, IList<Color> colors)
        {
            var distinctNumbers = numbers.Distinct().OrderByDescending(x => x);
            var colorMapping = new Dictionary<int, Color>();

            int partitionSize = distinctNumbers.Count() / colors.Count;

            int i = 0;
            int j = 0;

            foreach (var number in distinctNumbers)
            {
                colorMapping[number] = colors[j];
                if (i >= partitionSize)
                {
                    i = 0;
                    j++;
                }
                i++;
            }

            return colorMapping;
        }
    }
}
