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
        var algorithms = layouterAlgorithmFactories
            .SelectMany(_ => settings.LayouterAlgorithmSettings,
                (factory, algorithmSettings) => factory.Build(algorithmSettings))
            .Where(tuple => tuple.success)
            .Select(tuple => tuple.provider!)
            .ToList();

        var drawers = drawerFactories
            .SelectMany(_ => settings.DrawerSettings,
                (factory, drawerSettings) => factory.Build(drawerSettings))
            .Where(tuple => tuple.success)
            .Select(tuple => tuple.drawer!)
            .ToList();

        foreach (var algorithm in algorithms)
        foreach (var drawer in drawers)
        {
            var graphics = graphicsProvider.Create();
            var drawerInstance = drawer(graphics, algorithm);
            drawerInstance.DrawCloud(cloudWords);
            graphicsProvider.Commit();
        }
    }
}