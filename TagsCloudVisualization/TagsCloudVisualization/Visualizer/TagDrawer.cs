using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization.Visualizer
{
    internal class TagDrawer : IDrawer
    {
        public const int Border = 10;
        private readonly IDrawerOption options;
        private readonly IDrawableProvider<string> provider;
        private int Height;
        private int Width;

        public TagDrawer(IDrawableProvider<string> provider, IDrawerOption options)
        {
            this.provider = provider;
            this.options = options;
        }

        public Bitmap DrawImage()
        {
            var bitmap = PrepareBitmap(provider);
            var graphics = PrepareGraphics(bitmap, options.BackgroundColor);

            foreach (var drawable in provider.DrawableObjects)
            {
                var currentFont = new Font(options.TextOption.FontFamily, drawable.Place.Height / 2);
                graphics.DrawString(drawable.Value, currentFont, options.TextBrush,
                    drawable.Place);
            }

            return bitmap;
        }

        private Graphics PrepareGraphics(Bitmap bitmap, Color backgroundColor)
        {
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backgroundColor);

            graphics.TranslateTransform(Border / 2 - provider.TranslateTransform.X,
                Border / 2 - provider.TranslateTransform.Y);
            return graphics;
        }

        private Bitmap PrepareBitmap(IDrawableProvider<string> provider)
        {
            Width = provider.SizeOfCloud.Width + Border;
            Height = provider.SizeOfCloud.Height + Border;
            var bitmap = new Bitmap(Width, Height);
            return bitmap;
        }
    }
}