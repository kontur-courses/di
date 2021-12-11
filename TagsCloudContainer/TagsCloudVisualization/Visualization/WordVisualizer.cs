using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization.Configurator;

namespace TagsCloudVisualization.Visualization
{
#pragma warning disable CA1416
    public class WordVisualizer : IVisualizer
    {
        private readonly IVisualizingConfigurator configurator;
        private readonly ILayouter layouter;
        private readonly ScreenConfig screenConfig;
        private const int OneLetterPixelWidth = 7;
        private const int OneLetterPixelHeight = 8;

        public WordVisualizer(ILayouter layouter, IVisualizingConfigurator configurator, ScreenConfig screenConfig)
        {
            this.layouter = layouter;
            this.configurator = configurator;
            this.screenConfig = screenConfig;
        }

        public Bitmap Visualize(IEnumerable<string> visualizingValues)
        {
            var configured = configurator.Configure(visualizingValues);
            var placed = configured
                .Select(token => (layouter.PutNextRectangle(GetStrokeRectangleSize(token.FontSize)), token));
            
            var bitmap = new Bitmap(screenConfig.Size.Width, screenConfig.Size.Height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.DrawRectangle(new Pen(screenConfig.BackgroundColor),
                new Rectangle(new Point(0, 0), bitmap.Size));
            
            foreach (var (rectangle, token) in placed)
            {
                graphics.DrawString(token.Value,
                    new Font(FontFamily.GenericSansSerif, token.FontSize),
                    new SolidBrush(token.Color),
                    rectangle);
            }

            return bitmap;
        }

        private SizeF GetStrokeRectangleSize(int fontSize)
        {
            return new SizeF(OneLetterPixelWidth * fontSize, OneLetterPixelHeight * fontSize);
        }
    }
#pragma warning restore CA1416
}