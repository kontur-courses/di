using System.Collections.Generic;
using System.Drawing;
using TagCloud.Creators;

namespace TagCloud.Visualizers
{
    public class CloudVisualizer : IVisualizer
    {
        public Bitmap DrawCloud(IEnumerable<Tag> tags, 
            IDrawingSettings drawingSettings, 
            ITagColoring tagColoringAlgorithm)
        {
            var bitmap = drawingSettings.Bitmap;
            var graph = drawingSettings.Graphics;
            var backgroundBrush = drawingSettings.BackgroundColor;

            graph.Clear(backgroundBrush);

            foreach (var tag in tags)
            {
                var color = tagColoringAlgorithm.GetNextColor();
                using var brush = new SolidBrush(color);
                DrawTag(tag, graph, drawingSettings.Font, brush);
            }

            return bitmap;
        }

        private void DrawTag(Tag tag, Graphics graph, Font font, Brush pen)
        {
            using var drawFont = new Font(font.FontFamily, font.Size * tag.Frequency);
            graph.DrawString(tag.Value, drawFont, pen, tag.ContainingRectangle);
        }
    }
}
