using System;

namespace TagsCloudVisualization.Styling.WordSizeCalculators
{
    public class WordSizeCalculatorLogarithmic : WordSizeCalculator
    {
        public override float GetScaleFactor(int wordCount)
        {
            return (float) Math.Log(wordCount + 1);
        }
    }
}