using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class ClassicDriverFactory : IDrawerFactory
{
    public (Func<Graphics, Func<ILayouterAlgorithm>, IDrawer>? drawer, bool success) Build(
        DrawerSettings drawerSettings)
    {
        if (drawerSettings is not ClassicDrawerSettings cliGraphicsProviderSettings)
            return default;
        return ((graphics, provider) => new ClassicDrawer(cliGraphicsProviderSettings, graphics, provider), true);
    }
}