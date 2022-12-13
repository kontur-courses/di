using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ClassicDrawerFactory : IDrawerFactory
{
    public IDrawerProvider Build(
        DrawerSettings drawerSettings)
    {
        if (drawerSettings is not ClassicDrawerSettings cliGraphicsProviderSettings)
            return new EmptyDrawerProvider();
        return new ClassicDrawerProvider(cliGraphicsProviderSettings);
    }

    private class ClassicDrawerProvider : IDrawerProvider
    {
        private readonly ClassicDrawerSettings settings;

        public ClassicDrawerProvider(ClassicDrawerSettings settings)
        {
            this.settings = settings;
        }

        public IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics)
        {
            return new ClassicDrawer(settings, graphics, layouterAlgorithmProvider);
        }

        public bool CanProvide => true;
    }
}