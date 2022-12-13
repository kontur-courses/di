using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultRectangleArranger : IRectangleArranger
    {
        private ICloudLayouter cloudLayouter;
        public DefaultRectangleArranger(ICloudLayouter cloudLayouter)
        {
            this.cloudLayouter = cloudLayouter;
        }
        public List<TextContainer> GetContainers(Dictionary<string, int> words, Settings settings)
        {
            var textContainers = new List<TextContainer>();
            var rectangles = new List<Rectangle>();
            var defaultFont = settings.Font;
            cloudLayouter.StepSize = settings.StepSize;
            foreach (var word in words.Keys)
            {
                var font = new Font(defaultFont.FontFamily, defaultFont.Size + (words[word] - 1));
                var rectSize = word.MeasureString(font);
                var rect = cloudLayouter.GetNextRectangle(new Point(0, 0), rectangles, rectSize);
                rectangles.Add(rect);
                var container = new TextContainer(word, rect, font);
                textContainers.Add(container);
            }

            return textContainers;
        }
    }
}