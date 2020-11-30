using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.TagCloudBuilders
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly FontSettings fontSettings;
        
        public TagCloudBuilder(ICloudLayouter cloudLayouter, FontSettings fontSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.fontSettings = fontSettings;
        }

        public List<Tag> Build(Dictionary<string, int> wordsFrequency)
        {
            var tags = new List<Tag>();
            cloudLayouter.ClearLayout();
            foreach (var (word, frequency) in wordsFrequency)
            {
                var tagSize = GetTagSize(word, frequency);
                var rectangle = cloudLayouter.PutNextRectangle(tagSize);
                tags.Add(new Tag(word, rectangle, fontSettings.Font));
            }
            
            return tags;
        }

        private Size GetTagSize(string word, int freq)
        {
            //TODO: размер в зависимости от частоты
            return new Size((int)(word.Length * fontSettings.Font.Size / 1.5), (int)fontSettings.Font.Size * 2);
        }
    }
}