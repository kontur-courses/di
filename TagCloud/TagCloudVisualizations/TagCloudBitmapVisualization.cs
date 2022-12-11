﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TagCloud.TagCloudCreators;

namespace TagCloud.TagCloudVisualizations
{
    public class TagCloudBitmapVisualization : ITagCloudVisualization
    {
        private readonly Random random = new Random();
        private readonly ITagCloudCreator tagCloudCreator;
        private TagCloud tagCloud;

        public TagCloudBitmapVisualization(ITagCloudCreator tagCloudCreator)
        {
            this.tagCloudCreator = tagCloudCreator;
        }

        public void PrepareImage(Graphics inGraphics, ITagCloudVisualizationSettings settings)
        {
            PrepareTagCloud(settings);

            var bitmap = CreateTagCloudBitmap(settings);
            
            var rect = new Rectangle(0, 0, settings.PictureSize.Value.Width, settings.PictureSize.Value.Height);
            inGraphics.CompositingMode = CompositingMode.SourceCopy;
            inGraphics.CompositingQuality = CompositingQuality.HighQuality;
            inGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            inGraphics.SmoothingMode = SmoothingMode.HighQuality;
            inGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            inGraphics.DrawImage(bitmap, rect);
        }
        
        public void Save(string file, ITagCloudVisualizationSettings settings)
        {
            PrepareTagCloud(settings);

            var bitmap = CreateTagCloudBitmap(settings);

            if (settings.PictureSize != null)
                bitmap = new Bitmap(bitmap, settings.PictureSize.Value);

            bitmap.Save(file, settings.PictureFormat);
        }

        private void PrepareTagCloud(ITagCloudVisualizationSettings settings)
        {
            tagCloud = tagCloudCreator.GenerateTagCloud(settings);
        }

        private Bitmap CreateTagCloudBitmap(ITagCloudVisualizationSettings settings)
        {
            var rectangleOutline = 1;
            var bitmap = new Bitmap(
                tagCloud.GetWidth() + rectangleOutline,
                tagCloud.GetHeight() + rectangleOutline);

            var frameShift = new Size(-tagCloud.GetLeftBound(), -tagCloud.GetTopBound());
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
            foreach (var tag in tagCloud.Layouts)
            {
                tag.ShiftTo(frameShift);
                var byBrush = GetRandomBrush();
                tag.DrawIn(graphics, byBrush);
            }

            return bitmap;
        }

        private Brush GetRandomBrush() =>
            new SolidBrush(GetRandomColor());
        
        private Color GetRandomColor()
        {
            var knownColors = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            var randomColorName = knownColors[random.Next(knownColors.Length)];
            return Color.FromKnownColor(randomColorName);
        }
    }
}