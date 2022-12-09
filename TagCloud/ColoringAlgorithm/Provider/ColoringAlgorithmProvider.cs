namespace TagCloud.ColoringAlgorithm.Provider;

public static class ColoringAlgorithmProvider
{
    public static readonly IReadOnlyList<Type> Algorithms;
    
    static ColoringAlgorithmProvider()
    {
        var type = typeof(IColoringAlgorithm);
        Algorithms = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Contains(type) && t.IsClass)
            .ToArray();
    }
}