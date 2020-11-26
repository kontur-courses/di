using System;
using System.Drawing;

namespace TagsCloudVisualisation.Visualisation
{
    public class RectanglesVisualiser : BaseCloudVisualiser
    {
        private readonly uint scale;

        public RectanglesVisualiser(Point sourceCenterPoint, VisualisationDrawer drawer, uint scale)
            : base(sourceCenterPoint)
        {
            if (drawer == null)
                throw new ArgumentNullException(nameof(drawer));
            this.scale = scale;
            OnDraw += rect => drawer.Invoke(Graphics, rect);
        }

        public void Draw(Rectangle rectangle) => PrepareAndDraw(
            new RectangleF(rectangle.X * scale, rectangle.Y * scale,
                rectangle.Width * scale, rectangle.Height * scale));

        public static void DrawRectangle(Graphics g, Pen pen, RectangleF rectangle) =>
            g.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        public delegate void VisualisationDrawer(Graphics g, RectangleF rectangle);
    }
}