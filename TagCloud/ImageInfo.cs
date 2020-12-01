using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.FrequencyAnalyzer;
using TagCloud.Layout;

namespace TagCloud
{
    public class ImageInfo: IImageInfo
    {
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly ILayouter layouter;
        
        public ImageInfo(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter)
        {
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.layouter = layouter;
        }
        
        public List<Tuple<string, Rectangle>> GetTags(string filename, int canvasHeight)
        {
            var result = new List<Tuple<string, Rectangle>>();
            var frequencies = frequencyAnalyzer.GetFrequencyDictionary(filename);
            var orderedPairs = frequencies.OrderByDescending(pair => pair.Value);
            foreach (var pair in orderedPairs)
            {
                var height = (int)Math.Round(canvasHeight * pair.Value * Math.Sqrt(orderedPairs.Count()) / (3 * Math.PI));
                var width = (int)Math.Round((double)height * (pair.Key.Length - 1));
                var rectangle = layouter.PutNextRectangle(new Size(width, height));
                result.Add(new Tuple<string, Rectangle>(pair.Key, rectangle));
            }

            return result;
        }
    }
}