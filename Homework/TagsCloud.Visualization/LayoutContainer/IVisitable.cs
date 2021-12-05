using System.Drawing;
using TagsCloud.Visualization.ContainerVisitor;

namespace TagsCloud.Visualization.LayoutContainer
{
    public interface IVisitable
    {
        void Accept(Graphics graphics, IContainerVisitor visitor);
    }
}