using System;
using System.Drawing;
using System.Linq;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.ContainerVisitor
{
    public class RandomColorDrawerVisitor : IContainerVisitor
    {
        private readonly Random random = new();

        public void Visit(Graphics graphics, RectanglesContainer cont)
        {
            using var pen = new Pen(GetRandomColor());
            graphics.DrawRectangles(pen, cont.Items.ToArray());
        }

        public void Visit(Graphics graphics, WordsContainer container)
        {
            foreach (var (word, fontDecorator, rectangle) in container.Items)
            {
                var drawFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
                };
                using var brush = new SolidBrush(GetRandomColor());
                using var font = fontDecorator.Build();
                graphics.DrawString(word.Content,
                    font, brush, rectangle, drawFormat);
            }
        }

        private Color GetRandomColor()
            => Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }
}