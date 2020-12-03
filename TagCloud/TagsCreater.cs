using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.FrequencyAnalyzer;
using TagCloud.Layout;

namespace TagCloud
{
    public class TagsCreater: ITagsCreater
    {
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly ILayouter layouter;
        private const double heightCoefficient = 2.5 * Math.PI;
        private const double widthCoefficient = 0.7;
        
        public TagsCreater(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter)
        {
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.layouter = layouter;
        }
        
        public List<Tuple<string, Rectangle>> GetTags(string filename, int canvasHeight)
        {
            var frequencies = frequencyAnalyzer.GetFrequencyDictionary(filename);
            var tagsCount = frequencies.Count;
            return frequencies
                .OrderByDescending(pair => pair.Value)
                .Select(pair => GetTag(pair, canvasHeight, tagsCount))
                .ToList();
        }

        private Tuple<string, Rectangle> GetTag(KeyValuePair<string, double> pair, int canvasHeight, int tagsCount)
        {
            var frequency = pair.Value;
            var tagString = pair.Key;
            var height = (int)Math.Round(canvasHeight * frequency * Math.Sqrt(tagsCount) / heightCoefficient);
            var width = (int)Math.Round((double)height * (tagString.Length) * widthCoefficient);
            var rectangle = layouter.PutNextRectangle(new Size(width, height));
            return new Tuple<string, Rectangle>(tagString, rectangle);
        }
    }
}