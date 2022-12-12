using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public class MultiDrawer
{
    private readonly IEnumerable<IDrawerFactory> drawerFactories;
    private readonly IGraphicsProvider graphicsProvider;
    private readonly IEnumerable<ILayouterAlgorithmFactory> layouterAlgorithmFactories;
    private readonly Settings settings;

    public MultiDrawer(Settings settings, IEnumerable<IDrawerFactory> drawerFactories,
        IEnumerable<ILayouterAlgorithmFactory> layouterAlgorithmFactories,
        IGraphicsProvider graphicsProvider)
    {
        this.settings = settings;
        this.drawerFactories = drawerFactories;
        this.layouterAlgorithmFactories = layouterAlgorithmFactories;
        this.graphicsProvider = graphicsProvider;
    }

    public void Draw(IReadOnlyCollection<CloudWord> cloudWords)
    {
        foreach (var (algorithmProvider, drawerProvider) in GetDrawers())
        {
            DrawCloudUsingCombination(cloudWords, drawerProvider, algorithmProvider);
        }
    }

    private void DrawCloudUsingCombination(
        IReadOnlyCollection<CloudWord> cloudWords,
        IDrawerProvider drawerProvider,
        ILayouterAlgorithmProvider algorithmProvider)
    {
        var graphics = graphicsProvider.Create();
        var drawer = drawerProvider.Provide(algorithmProvider, graphics);
        drawer.DrawCloud(cloudWords);
        graphicsProvider.Commit();
    }

    private IEnumerable<(ILayouterAlgorithmProvider algorithmProvider, IDrawerProvider drawerProvider)> GetDrawers()
    {
        var (algorithmProviders, drawerProviders) = GetProviders();

        return algorithmProviders
            .SelectMany(_ => drawerProviders,
                (algorithmProvider, drawerProvider) => (algorithmProvider, drawerProvider));
    }

    private (List<ILayouterAlgorithmProvider>, List<IDrawerProvider>) GetProviders()
    {
        var algorithmProviders = layouterAlgorithmFactories
            .SelectMany(_ => settings.LayouterAlgorithmSettings,
                (factory, algorithmSettings) => factory.Build(algorithmSettings))
            .Where(tuple => tuple.CanProvide)
            .ToList();

        var drawerProviders = drawerFactories
            .SelectMany(_ => settings.DrawerSettings,
                (factory, drawerSettings) => factory.Build(drawerSettings))
            .Where(drawerProvider => drawerProvider.CanProvide)
            .ToList();
        return (algorithmProviders, drawerProviders);
    }
}