using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    internal class TagDrawer : IDrawer<string>
    {
        public const int Border = 10;

        private int height;
        private int width;

        public Bitmap GetBitmap(CloudInfo<string> cloudInfo, DrawerSettings settings)
        {
            var heightCoefficient = (float) cloudInfo.SizeOfCloud.Height / (settings.Heigth - Border);
            var widthCoefficient = (float) cloudInfo.SizeOfCloud.Width / (settings.Width - Border);
            var biggestCoefficient = Math.Max(heightCoefficient, widthCoefficient);

            var bitmap = PrepareBitmap(settings);
            var graphics = PrepareGraphics(bitmap, cloudInfo, settings.BackgroundColor, biggestCoefficient);

            foreach (var drawable in cloudInfo.DrawableSource)
            {
                var currentFont = new Font(settings.TextFont.FontFamily,
                    (int) Math.Round(drawable.Place.Height / (biggestCoefficient * 2)));
                var currentPlace = new PointF(drawable.Place.X / biggestCoefficient,
                    drawable.Place.Y / biggestCoefficient);
                graphics.DrawString(drawable.Value, currentFont, settings.TextBrush,
                    currentPlace);
            }

            return bitmap;
        }

        private Graphics PrepareGraphics(Bitmap bitmap, CloudInfo<string> cloudInfo, Color backgroundColor,
            float coefficient)
        {
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backgroundColor);

            graphics.TranslateTransform(Border / 2 - cloudInfo.TranslateTransform.X / coefficient,
                Border / 2 - cloudInfo.TranslateTransform.Y / coefficient);
            return graphics;
        }

        private Bitmap PrepareBitmap(DrawerSettings settings)
        {
            width = settings.Width;
            height = settings.Heigth;
            var bitmap = new Bitmap(width, height);
            return bitmap;
        }
    }
}