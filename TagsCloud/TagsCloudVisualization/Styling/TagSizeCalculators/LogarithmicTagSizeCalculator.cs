using System;

namespace TagsCloudVisualization.Styling.TagSizeCalculators
{
    public class LogarithmicTagSizeCalculator : TagSizeCalculator
    {
        public override float GetScaleFactor(int wordCount, int minFontSize)
        {
            return (float) (Math.Log(wordCount + 1) * minFontSize);
        }
    }
}