using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Providers.Layouter;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    internal class TagDrawer : IDrawer
    {
        private const int Border = 10;

        private int height;
        private int width;

        public Bitmap GetBitmap(CloudInfo cloudInfo, DrawerSettings drawerSettings)
        {
            var heightCoefficient = (float) cloudInfo.SizeOfCloud.Height / (drawerSettings.Height - Border);
            var widthCoefficient = (float) cloudInfo.SizeOfCloud.Width / (drawerSettings.Width - Border);
            var biggestCoefficient = Math.Max(heightCoefficient, widthCoefficient);

            var bitmap = PrepareBitmap(drawerSettings);
            var graphics = PrepareGraphics(bitmap, cloudInfo, drawerSettings.BackgroundColor, biggestCoefficient);

            foreach (var drawable in cloudInfo.DrawableSource)
            {
                using var currentFont = new Font(drawerSettings.TextFont.FontFamily,
                    (int) Math.Round(drawable.Place.Height / (biggestCoefficient * 2)));
                var currentPlace = new PointF(drawable.Place.X / biggestCoefficient,
                    drawable.Place.Y / biggestCoefficient);
                graphics.DrawString(drawable.Value, currentFont, drawerSettings.TextBrush,
                    currentPlace);
            }

            return bitmap;
        }

        private Graphics PrepareGraphics(Bitmap bitmap, CloudInfo cloudInfo, Color backgroundColor,
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
            height = settings.Height;
            var bitmap = new Bitmap(width, height);
            return bitmap;
        }
    }
}