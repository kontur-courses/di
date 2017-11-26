using System;

namespace TagsCloudVisualization
{
    public class FontSizeMaker:IFontSizeMaker
    {
        private readonly int minSize;
        private readonly int maxSize;

        public FontSizeMaker(int minSize, int maxSize)
        {
            this.minSize = minSize;
            this.maxSize = maxSize;
        }
        public int GetFontSizeByFreq(int maxFreq, int frequency)
        {
            return (int)(((double)frequency/maxFreq)*(maxSize-minSize) + minSize);
        }
        
    }
}