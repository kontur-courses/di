using System.Drawing;
using TagsCloud.Visualization.LayoutContainer;

namespace TagsCloud.Visualization.ContainerVisitor
{
    public interface IContainerVisitor
    {
        void Visit(Graphics graphics, RectanglesContainer cont);
        void Visit(Graphics graphics, WordsContainer container);
    }
}