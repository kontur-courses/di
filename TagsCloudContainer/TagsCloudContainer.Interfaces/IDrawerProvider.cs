using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface IDrawerProvider
{
    bool CanProvide { get; }
    IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics);
}