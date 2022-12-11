namespace TagsCloudContainer.Interfaces;

public class Settings
{
    public IReadOnlyCollection<DrawerSettings> DrawerSettings { get; set; } = new List<DrawerSettings>();

    public IReadOnlyCollection<LayouterAlgorithmSettings> LayouterAlgorithmSettings { get; set; } =
        new List<LayouterAlgorithmSettings>();

    public GraphicsProviderSettings GraphicsProviderSettings { get; set; } = null!;
}