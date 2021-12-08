using System.Drawing;
using System.Linq;
using TagsCloud.Visualization.Drawers;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.ContainerVisitor
{
    public class ConcreteColorDrawerVisitor : IContainerVisitor
    {
        private readonly ImageSettings settings;

        public ConcreteColorDrawerVisitor(ImageSettings settings) => this.settings = settings;

        public void Visit(Graphics graphics, RectanglesContainer cont)
        {
            using var pen = new Pen(settings.Color);
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
                using var brush = new SolidBrush(settings.Color);
                using var font = fontDecorator.Build();
                graphics.DrawString(word.Content,
                    font, brush, rectangle, drawFormat);
            }
        }
    }
}