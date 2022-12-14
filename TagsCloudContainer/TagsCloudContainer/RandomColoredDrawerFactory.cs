using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class RandomColoredDrawerFactory : IDrawerFactory
{
    public IDrawerProvider Build(DrawerSettings drawerSettings)
    {
        if (drawerSettings is not RandomColoredDrawerSettings settings)
            return new EmptyDrawerProvider();
        return new DrawerProvider(settings);
    }

    private class DrawerProvider : IDrawerProvider
    {
        private readonly RandomColoredDrawerSettings settings;

        public DrawerProvider(RandomColoredDrawerSettings settings)
        {
            this.settings = settings;
        }

        public bool CanProvide => true;

        public IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics)
        {
            return new RandomColoredDrawer(settings, graphics, layouterAlgorithmProvider);
        }
    }
}