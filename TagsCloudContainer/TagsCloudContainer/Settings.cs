namespace TagsCloudContainer;

public class Settings
{
    public IReadOnlyCollection<DrawerSettings> DrawerSettings { get; set; }
        = new List<DrawerSettings> { new ClassicDrawerSettings() };

    public IReadOnlyCollection<LayouterAlgorithmSettings> LayouterAlgorithmSettings { get; set; } =
        new List<LayouterAlgorithmSettings> { new CircularLayouterAlgorithmSettings() };

    public GraphicsProviderSettings GraphicsProviderSettings { get; set; } = new CliGraphicsProviderSettings();
}