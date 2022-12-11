using System;
using System.Drawing;
using TagCloud.Tags;

namespace TagCloud.TagCloudVisualizations
{
    public class WordTagCloudBitmapVisualization : TagCloudBitmapVisualization
    {
        protected override void DrawIn(Graphics graphics, ITag tag, Brush byBrush)
        {
            var word = tag as Word;
            if (word == null)
                throw new NullReferenceException("wrong type of tag");
            graphics.DrawString(word.Text, word.Font, byBrush, word.Frame.Location);
        }
    }
}
