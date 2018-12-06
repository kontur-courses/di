using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace TagsCloudContainer.CircularCloudLayouter
{
    internal class RectangleVisualizer: IDrawer<Rectangle>
    {
        private readonly CircularCloudLayout _layout;
        private Bitmap _bitmap;

        public RectangleVisualizer(CircularCloudLayout layout)
        {
            _layout = layout;
            CreateBitmapForDrawing(_layout.ImageSize());
        }

        public void DrawTagsCloud(string path)
        {
            var g = Graphics.FromImage(_bitmap);
            DrawAllRectangles(g);

            _bitmap.Save(path);
        }

        private void DrawAllRectangles(Graphics g)
        {
            foreach (var rectangle in _layout.GetCoordinatesToDraw())
                g.FillRectangle
                (
                    TakeRandomColor(),
                    rectangle.X, rectangle.Y,
                    rectangle.Size.Width,
                    rectangle.Size.Height
                );
        }

        private static Brush TakeRandomColor()
        {
            var rnd = new Random();
            var color = Color.FromArgb(rnd.Next());

            var brush = new SolidBrush(color);
            Thread.Sleep(25);

            return brush;
        }

        private void CreateBitmapForDrawing(Size imageSize)
        {
            _bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        }

        public void DrawItems(IEnumerable<ItemToDraw<Rectangle>> itemsToDraws)
        {
            throw new NotImplementedException();
        }
    }
}