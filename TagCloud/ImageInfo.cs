using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Layout;

namespace TagCloud
{
    public class ImageInfo: IImageInfo
    {
        private IFrequencyAnalyzer FrequencyAnalyzer;
        private ILayouter Layouter;
        
        public ImageInfo(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter)
        {
            FrequencyAnalyzer = frequencyAnalyzer;
            Layouter = layouter;
        }
        
        public List<Tuple<string, Rectangle>> GetTags(string filename, int canvasHeight)
        {
            var result = new List<Tuple<string, Rectangle>>();
            var frequencies = FrequencyAnalyzer.GetFrequencyDictionary(filename);
            var orderedPairs = frequencies.OrderByDescending(pair => pair.Value);
            foreach (var pair in orderedPairs)
            {
                var height = (int)Math.Round(canvasHeight * pair.Value);
                var width = (int)Math.Round((double)height * pair.Key.Length / 2);
                var rectangle = Layouter.PutNextRectangle(new Size(width, height));
                result.Add(new Tuple<string, Rectangle>(pair.Key, rectangle));
            }

            return result;
        }
    }
}