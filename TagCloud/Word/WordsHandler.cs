using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.CloudLayouter;

namespace TagsCloudVisualization
{
    public class WordsHandler
    {
        public ICloudLayouter CloudLayouter { get; }

        public WordsHandler(ICloudLayouter cloudLayouter)
        {
            CloudLayouter = cloudLayouter;
        }

        public Template Handle(List<(string, Font)> words)
        {
            var template = new Template();
            var fakeImage = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(fakeImage);
            foreach (var (word, font) in words)
            {
                var wordSize = graphics.MeasureString(word, font).ToSize() + new Size(1, 1);
                var wordRectangle = CloudLayouter.PutNextRectangle(wordSize);
                template.Add(new WordParameter(word, wordRectangle, font));
            }

            template.Size = CloudLayouter.SizeF.ToSize();
            template.Center = CloudLayouter.Center;
            return template;
        }
    }
}