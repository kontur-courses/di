using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface IDrawerProvider
{
    IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics);
    
    bool CanProvide { get; }
}