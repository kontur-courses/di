using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication
{
    public class TagCloudCreator
    {
        private readonly ICloudLayouter layouter;
        private readonly IWordKeeper wordKeeper;
        private readonly Size tagCloudSize;

        public double PrecisionFactor { get; }


        public TagCloudCreator(ICloudLayouter layouter,
                IWordKeeper wordKeeper, 
                Size tagCloudSize,
                double precisionFactor = 0.005)
        {
            this.layouter = layouter;
            this.wordKeeper = wordKeeper;
            this.tagCloudSize = tagCloudSize;
            PrecisionFactor = precisionFactor;
        }

        public TagCloud BuildTagCloudBy(string source)
        {
            var rects = GetRectangles(wordKeeper.GetWordFrequency(source));             
            return new TagCloud(rects, tagCloudSize);
        }

        private List<(string, Rectangle)> GetRectangles(Dictionary<string, int> wordFreq)
        {         
            var res = new List<(string, Rectangle)>();
            foreach (var pair in wordFreq)
            {
                var size = GetWordSize(pair.Key.Length, pair.Value);
                var rect = layouter.PutNextRectangle(size);
                res.Add((pair.Key, rect));
            }

            return res;
        }

        private Size GetWordSize(int wordLenght, int frequency) =>
            new Size((int)(frequency * tagCloudSize.Width * PrecisionFactor * wordLenght),
                (int)(frequency * tagCloudSize.Height * PrecisionFactor));
            
    }

}
