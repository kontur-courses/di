using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloudCreator
{
    public class RectanglesForWordsCreator
    {
        private List<(string, Rectangle)> GetRectangles(IEnumerable<(string, int)> wordsStatistic)
        {
            var statistic = wordsStatistic.ToList();
            var maxCount = statistic.Max(x => x.Item2);
            var minCount = statistic.Max(x => x.Item2);
            //Graphics.FromImage(new Bitmap(0,0)).MeasureString("",new Font(FontFamily.GenericMonospace, 12))
            throw new NotImplementedException();
        }
    }
}