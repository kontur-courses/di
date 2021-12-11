using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization.Configurator;
#pragma warning disable CA1416
namespace TagsCloudVisualization.Visualization
{
    public class WordsVisualizer : IVisualizer
    {
        private readonly IVisualizingConfigurator configurator;
        private readonly ILayouter layouter;
        private readonly ScreenConfig screenConfig;

        public WordsVisualizer(ILayouter layouter, IVisualizingConfigurator configurator, ScreenConfig screenConfig)
        {
            this.layouter = layouter;
            this.configurator = configurator;
            this.screenConfig = screenConfig;
        }

        public Bitmap Visualize(IEnumerable<string> visualizingValues)
        {
            var bitmap = new Bitmap(screenConfig.Size.Width, screenConfig.Size.Height);
            
            using var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(screenConfig.BackgroundColor), 
                new Rectangle(new Point(0, 0), bitmap.Size));
            
            var configured = configurator.Configure(visualizingValues);
            
            var placed = configured
                .Select(token => (
                    layouter.PutNextRectangle(graphics.MeasureString(token.Value, token.Font)),
                    token));

            foreach (var (rectangle, token) in placed)
            {
                graphics.DrawString(token.Value, token.Font,
                    new SolidBrush(token.Color),
                    rectangle);
            }

            return bitmap;
        }
    }
}