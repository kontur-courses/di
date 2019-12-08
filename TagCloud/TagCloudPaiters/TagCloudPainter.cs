using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization;

namespace TagCloud
{
    public class TagCloudPainter : ITagCloudPainter
    {
        private readonly IPainterConfig painterConfig;
        private readonly IEnumerable<TagInfo> tagInfos;
        
        public TagCloudPainter(IPainterConfig config, IEnumerable<TagInfo> tagInfos)
        {
            painterConfig = config;
            this.tagInfos = tagInfos;
        }
        
        public void Draw()
        {
            var canvas = new Canvas(painterConfig.ImageWidth, painterConfig.ImageHeight);
            var cloudLayouter = new CircularCloudLayouter(painterConfig.CloudCenter);
            
            var sortedTagInfos = tagInfos.OrderBy(tagInfo => tagInfo.Frequency).ToList();
            var maxFrequency = sortedTagInfos.Last().Frequency;
            var minFrequency = sortedTagInfos.First().Frequency;
            
            foreach (var tagInfo in sortedTagInfos)
            {
                var tagFontSize = GetTagFontSize(tagInfo, painterConfig, maxFrequency, minFrequency);
                var rec = cloudLayouter.PutNextRectangle(GetTagSize(tagInfo, tagFontSize, painterConfig.FontFamily));
                canvas.Draw(tagInfo.Tag.Content,new Font(painterConfig.FontFamily, tagFontSize),  rec);
            }
            
            canvas.Save(painterConfig.PathForSave, painterConfig.ImageName);
        }

        private int GetTagFontSize(TagInfo tagInfo, IPainterConfig config, int maxFrequency, int minFrequency)
        {
            var fontSize = config.MinFontSize + ((config.MaxFontSize - config.MinFontSize) * tagInfo.Frequency) / (maxFrequency - minFrequency);
            return fontSize;
        }

        private Size GetTagSize(TagInfo tagInfo, int fontSize, FontFamily family)
        {
            var font = new Font(family, fontSize);
            return TextRenderer.MeasureText(tagInfo.Tag.Content, font);
        }
    }
}