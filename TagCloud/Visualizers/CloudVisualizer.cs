using System.Collections.Generic;
using System.Drawing;
using TagCloud.Layouters;

namespace TagCloud.Visualizers
{
    public class CloudVisualizer : IVisualizer
    {
        //private readonly Bitmap bitmap;
        //private readonly Graphics graph;
        //private readonly Brush pen;
        //private readonly Font font;
        //private readonly Color backgroundBrush;

        //public CloudVisualizer(IDrawingSettings drawingSettings)
        //{   
        //    this.font = drawingSettings.Font;
        //    this.bitmap = drawingSettings.Bitmap;
        //    this.graph = drawingSettings.Graphics;
        //    this.pen = drawingSettings.PenBrush;
        //    this.backgroundBrush = drawingSettings.BackgroundColor;
        //}

        public Bitmap DrawCloud(IEnumerable<Tag> tags, IDrawingSettings drawingSettings)
        {
            var bitmap = drawingSettings.Bitmap;
            var graph = drawingSettings.Graphics;
            var backgroundBrush = drawingSettings.BackgroundColor;

            graph.Clear(backgroundBrush);

            foreach (var tag in tags)
                DrawTag(tag, graph, drawingSettings.Font, drawingSettings.PenBrush);

            return bitmap;
        }

        private void DrawTag(Tag tag, Graphics graph, Font font, Brush pen)
        {
            using var drawFont = new Font(font.FontFamily, font.Size * tag.Frequency);
            graph.DrawString(tag.Value, drawFont, pen, tag.ContainingRectangle);
        }
    }
}
