using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.TagCloudVisualizers
{
    public class TagCloudVisualizer : ICloudVisualizer
    {
        private readonly Palette palette;
        private readonly ICanvas canvas;

        public TagCloudVisualizer(ICanvas canvas, Palette palette)
        {
            this.canvas = canvas;
            this.palette = palette;
        }

        public void PrintTagCloud(List<Tag> tags)
        {
            var imageSize = canvas.GetImageSize();
            using (var graphics = canvas.StartDrawing())
            {
                using (var backgroundBrush = new SolidBrush(palette.BackgroundColor))
                    graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);

                using (var textBrush = new SolidBrush(palette.TextColor))
                {
                    DrawCloud(tags, textBrush, graphics);
                }
            }

            canvas.UpdateUi();
        }

        private void DrawCloud(List<Tag> tags, SolidBrush textBrush, Graphics graphics)
        {
            var pen = new Pen(Color.White);
            var iterationCount = 0;
            foreach (var tag in tags)
            {
                if (palette.EnabledMultipleColors)
                    textBrush.Color = palette.Colors[iterationCount % palette.Colors.Length];
                
                graphics.DrawString(tag.Text, tag.Font, textBrush, tag.BoundingZone.Location);

                if (palette.DrawTextBoundingZone)
                {
                    pen.Color = Color.Beige;
                    graphics.DrawRectangle(pen, tag.BoundingZone);
                }

                iterationCount++;
            }
        }
    }
}