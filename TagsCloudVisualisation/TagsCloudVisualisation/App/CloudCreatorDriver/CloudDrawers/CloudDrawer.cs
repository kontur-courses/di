using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.App.CloudCreatorDriver.DrawingSettings;
using TagsCloudVisualisation.App.CloudDrawers.Exceptions;
using TagsCloudVisualisation.App.CloudDrawers.WordToDraw;
using TagsCloudVisualisation.App.DrawingSettings;

namespace TagsCloudVisualisation.App.CloudDrawers
{
    public class CloudDrawer : ICloudDrawer
    {
        public Bitmap DrawWords(List<IDrawingWord> words, IDrawingSettings settings)
        {
            var minExpectedSize = FindSizeByRectangles(words);
            if (minExpectedSize.Height > settings.PictureSize.Height
                || minExpectedSize.Width > settings.PictureSize.Width)
                throw new DrawingException($"User sizes less then min required sizes " +
                                           $"{minExpectedSize} > {settings.PictureSize}");
            return Draw(words, settings.PictureSize.Width, settings.PictureSize.Height, settings.BgColor);
        }
        
        private static Size FindSizeByRectangles(IReadOnlyCollection<IDrawingWord> words)
        {
            var width = words.Max(word => word.Rectangle.Right);
            var height = words.Max(word => word.Rectangle.Bottom);
            return new Size(width, height);
        }
        
        private static Bitmap Draw(
            IEnumerable<IDrawingWord> drawingWords,
            int width, int height,
            Color bgColor)
        {
            var myBitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(myBitmap);
            graphics.Clear(bgColor);

            foreach (var word in drawingWords)
            {
                if (word == null)
                    throw new DrawingException("Word can not be null", new NullReferenceException());
                graphics.DrawString(
                    word.Value,
                    word.Font,
                    new SolidBrush(word.Color),
                    word.Rectangle);
            }

            return myBitmap;
        }
    }
}