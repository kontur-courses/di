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
        private readonly Size imageSize;
        private readonly int scale;

        public TagCloudCreator(ICloudLayouter layouter,
                IWordKeeper wordKeeper, Size imageSize)
        {
            this.layouter = layouter;
            this.wordKeeper = wordKeeper;
            this.imageSize = imageSize;
            var mainSide = imageSize.Height < imageSize.Width ? imageSize.Height : imageSize.Width;
            scale = mainSide / 100;
        }

        public TagCloud BuildTagCloudBy(string sourceFileName)
        {
            var rects = GetRectangles(wordKeeper.GetWordIncidenceInPercent(sourceFileName, 5));             
            return new TagCloud(rects, new Size(imageSize.Width+10, imageSize.Height+10));
        }

        private List<(string, Rectangle)> GetRectangles(List<(string, int)> wordFreq)
        {         
            return wordFreq
                .Select(p => (p.Item1,GetWordSize(p.Item1.Length, p.Item2)))
                .Where(p => !p.Item2.IsEmpty)
                .OrderByDescending(p => p.Item2.Height * p.Item2.Width)
                .Select(p => (p.Item1, layouter.PutNextRectangle(p.Item2)))
                .ToList();
        }

        private Size GetWordSize(int wordLength, int frequency) =>
            new Size((frequency * wordLength / 2 * scale), (frequency*scale));
        
                  
    }

}
