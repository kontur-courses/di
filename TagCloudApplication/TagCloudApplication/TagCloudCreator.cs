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


        public TagCloudCreator(ICloudLayouter layouter,
                IWordKeeper wordKeeper)
        {
            this.layouter = layouter;
            this.wordKeeper = wordKeeper;
        }

        public TagCloud BuildTagCloudBy(string source)
        {
            var rects = GetRectangles(wordKeeper.GetWordIncidence(source));             
            return new TagCloud(rects);
        }

        private List<(string, Rectangle)> GetRectangles(List<(string, int)> wordFreq)
        {         
            var res = new List<(string, Rectangle)>();
            foreach (var pair in wordFreq)
            {
                var size = GetWordSize(pair.Item1.Length, pair.Item2);
                var rect = layouter.PutNextRectangle(size);
                res.Add((pair.Item1, rect));
            }

            return res;
        }

        private Size GetWordSize(int wordLenght, int frequency) =>
            new Size((frequency * wordLenght / 2), (frequency));           
    }

}
