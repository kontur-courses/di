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
        private readonly ClassicDrawerSettings cliGraphicsProviderSettings;

        public ClassicDrawerProvider(ClassicDrawerSettings cliGraphicsProviderSettings) =>
            this.cliGraphicsProviderSettings = cliGraphicsProviderSettings;

        public IDrawer Provide(ILayouterAlgorithmProvider layouterAlgorithmProvider, Graphics graphics) =>
            new ClassicDrawer(cliGraphicsProviderSettings, graphics, layouterAlgorithmProvider);

        public bool CanProvide => true;
    }
}