using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;


namespace TagCloud
{
    public class CloudDrawer
    {
        private readonly ICloudLayouter layouter;
        private readonly DrawingSettings settings;

        //TODO Tests?
        public CloudDrawer(ICloudLayouter layouter, DrawingSettings settings)
        {
            this.layouter = layouter;
            this.settings = settings;
        }

        public Bitmap Draw(IEnumerable<(string word, int fontSize)> wordWeightPairs)
        {
            var map = new Bitmap(settings.Size.Height,settings.Size.Width);
            using (var g = Graphics.FromImage(map))
            {
                g.FillRegion(settings.BackgroundBrush, g.Clip);
                foreach (var t in wordWeightPairs)
                {
                    var font = new Font(settings.FontType, t.fontSize);
                    var rectangleSize = g.MeasureString(t.word, font).ToSize();
                    var rect = layouter.PutNextRectangle(rectangleSize);
                    g.DrawString(t.word, font, settings.FontBrush, rect);
                }
            }
            return map;
        }
    }
}