using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class ImageCreator
    {
        private readonly Bitmap bitmap;

        public int Width { get; }
        public int Height { get; }
        public Graphics Graphics { get; }

        public ImageCreator(int width, int height)
        {
            Width = width;
            Height = height;

            bitmap = new Bitmap(width, height);
            Graphics = Graphics.FromImage(bitmap);
        }

        public void DrawCloud(IEnumerable<ICloudItem> cloudItems,
            Color foregroundColor, Color backgroundColor)
        {
            var foregroundBrush = new SolidBrush(foregroundColor);
            var backgroundBrush = new SolidBrush(backgroundColor);

            Graphics.FillRectangle(backgroundBrush, 0, 0, Width, Height);
            foreach (var item in cloudItems)
            {
                Graphics.DrawString(
                    item.Word, item.Font, foregroundBrush, item.Rectangle);
            }
        }

        public void Save(string path) => bitmap.Save(path);
    }
}