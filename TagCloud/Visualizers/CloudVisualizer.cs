using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.Layouters;

namespace TagCloud.Visualizers
{
    public class CloudVisualizer : IVisualizer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graph;
        private readonly Brush pen;
        private readonly Font font;
        private readonly Color backgroundBrush;

        public CloudVisualizer(IDrawingSettings drawingSettings)
        {
            this.font = drawingSettings.Font;
            this.bitmap = drawingSettings.Bitmap;
            this.graph = drawingSettings.Graphics;
            this.pen = drawingSettings.PenBrush;
            this.backgroundBrush = drawingSettings.BackgroundColor;
        }

        public Bitmap DrawCloud(IEnumerable<Tag> tags)
        {
            graph.Clear(backgroundBrush);

            foreach (var tag in tags)
                DrawTag(tag);

            return bitmap;
        }

        private void DrawTag(Tag tag)
        {
            using var drawFont = new Font(font.FontFamily, font.Size * tag.Frequency);
            graph.DrawString(tag.Value, drawFont, pen, tag.ContainingRectangle);
        }

        public void Dispose()
        {
            bitmap?.Dispose();
            graph?.Dispose();
            pen?.Dispose();
            font?.Dispose();
        }
    }
}
