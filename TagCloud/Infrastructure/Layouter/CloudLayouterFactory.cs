namespace TagCloud.Infrastructure.Layouter;

public class CloudLayouterFactory : ICloudLayouterFactory
{
    private readonly ICloudLayouter defaultLayouter;

    private readonly IReadOnlyDictionary<string, ICloudLayouter> layouters;

    public CloudLayouterFactory(IEnumerable<ICloudLayouter> layouters, ICloudLayouter defaultLayouter)
    {
        this.defaultLayouter = defaultLayouter;
        this.layouters = CreateLayoutersDictionary(layouters);
    }

    public ICloudLayouter Create(string layouterName)
    {
        var key = layouterName.ToLowerInvariant();

        return layouters.ContainsKey(key)
            ? layouters[key]
            : defaultLayouter;
    }

    private static Dictionary<string, ICloudLayouter> CreateLayoutersDictionary(IEnumerable<ICloudLayouter> layouters)
    {
        var dictionary = new Dictionary<string, ICloudLayouter>();

        foreach (var layouter in layouters)
        {
            var layouterName = layouter.GetType().Name.ToLowerInvariant();
            var position = layouterName.IndexOf("cloudlayouter", StringComparison.Ordinal);
            dictionary[layouterName[..position]] = layouter;
        }

        return dictionary;
    }
}