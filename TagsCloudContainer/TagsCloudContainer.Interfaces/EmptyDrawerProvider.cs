using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public class EmptyDrawerProvider : IDrawerProvider
{
    public IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics) =>
        throw new NotImplementedException();

    public bool CanProvide => false;
}