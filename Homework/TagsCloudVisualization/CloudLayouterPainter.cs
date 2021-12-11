using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal static class CloudLayouterPainter
    {
        private static readonly List<Brush> BrushList = new List<Brush>
        {
            Brushes.Blue, Brushes.Aquamarine, Brushes.BlueViolet,
            Brushes.CornflowerBlue, Brushes.DarkBlue, Brushes.DarkCyan,
            Brushes.Indigo, Brushes.SteelBlue, Brushes.SlateBlue,
            Brushes.Purple, Brushes.SkyBlue, Brushes.Navy, Brushes.DarkCyan,
            Brushes.DarkSlateGray, Brushes.DarkOrchid, Brushes.MediumAquamarine,
            Brushes.MidnightBlue
        };

        public static void Draw(CircularCloudLayouter layouter, string imagePathToSave)
        {
            const double imageSizeLimit = 5000;
            const double border = 100;
            var cloudEnclosingCircleRadius = layouter.EnclosingRadius;
            var imageRadius = (int)Math.Min(cloudEnclosingCircleRadius + border, imageSizeLimit);
            var imageCenter = new Point(imageRadius, imageRadius);
            var imageSize = 2 * imageRadius;
            var imageShift = layouter.CloudCenter.GetShiftTo(imageCenter);

            var image = new Bitmap(imageSize, imageSize);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black);

            foreach (var rect in layouter.Rectangles)
            {
                var shiftedLocation = rect.Location.AddShift(imageShift);
                graphics.FillRectangle(GetRandomBrush(),
                    new Rectangle(shiftedLocation, rect.Size));
            }
            image.Save(imagePathToSave);
        }

        private static Brush GetRandomBrush()
        {
            var randomBrushNumber = new Random().Next(BrushList.Count);
            return BrushList[randomBrushNumber];
        }
    }
}
