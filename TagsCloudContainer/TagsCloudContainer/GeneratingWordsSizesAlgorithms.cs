using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class GeneratingWordsSizesAlgorithms
    {
        private const double scaleCoefficient = 500;
        private const int initialWordHeight = 400;
        private const int minHeight = 30;
        private const int minWidth = 30;

        public static IEnumerable<Tuple<string, Size>> DefaultAlgorithm(Dictionary<string, int> wordsFrequency)
        {
            var result = new List<Tuple<string, Size>>();
            double maxFrequency = wordsFrequency.Max(pair => pair.Value);
            foreach (var pair in wordsFrequency.OrderBy(pair => pair.Value).Reverse())
            {
                var curFrequency = pair.Value;
                var frequencyRatio = Math.Min(0.05 + curFrequency / maxFrequency, 1.0);
                var curHeight = Math.Max((int)(initialWordHeight * frequencyRatio), minHeight);
                var curWidth = (int)(pair.Key.Length * frequencyRatio * scaleCoefficient);
                if (curWidth > (curHeight / 2 * pair.Key.Length))
                    curWidth = (curHeight / 2 * pair.Key.Length);
                curWidth = Math.Max(curWidth, minWidth);
                result.Add(new Tuple<string, Size>(pair.Key, new Size(curWidth, curHeight)));
            }
            return result;
        }
    }
}