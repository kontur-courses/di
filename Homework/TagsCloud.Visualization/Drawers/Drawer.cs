using System;
using System.Drawing;
using System.Linq;
using TagsCloud.Visualization.ContainerVisitor;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.Drawers
{
    public class Drawer : IDrawer
    {
        private const int OffsetX = 100;
        private const int OffsetY = 100;
        private readonly IContainerVisitor visitor;

        public Drawer(IContainerVisitor visitor) => this.visitor = visitor;

        public Image Draw<T>(ILayoutContainer<T> layoutContainer)
        {
            if (!layoutContainer.Items.Any())
                throw new ArgumentException("rectangles array can't be empty");

            var (width, height) = layoutContainer.GetWidthAndHeight();
            var (widthWithOffset, heightWithOffset) = (width + OffsetX, height + OffsetY);
            var center = layoutContainer.GetCenter();
            var bitmap = new Bitmap(widthWithOffset, heightWithOffset);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.TranslateTransform(center.X + widthWithOffset / 2, center.Y + heightWithOffset / 2);

            layoutContainer.Accept(graphics, visitor);

            return bitmap;
        }
    }
}