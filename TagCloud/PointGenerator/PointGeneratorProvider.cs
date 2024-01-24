namespace TagCloud.PointGenerator;

public class PointGeneratorProvider : IPointGeneratorProvider
{
    private Dictionary<string, IPointGenerator> registeredGenerators;

    public PointGeneratorProvider(IEnumerable<IPointGenerator> generators)
    {
        registeredGenerators = ArrangeLayouters(generators);
    }

    public IPointGenerator CreateGenerator(string generatorName)
    {
        if (registeredGenerators.ContainsKey(generatorName))
            return registeredGenerators[generatorName];
        throw new ArgumentException($"{generatorName} layouter is not supported");
    }

    private Dictionary<string, IPointGenerator> ArrangeLayouters(IEnumerable<IPointGenerator> generators)
    {
        var generatorsDictionary = new Dictionary<string, IPointGenerator>();
        foreach (var generator in generators)
        {
            generatorsDictionary[generator.GeneratorName] = generator;
        }

        return generatorsDictionary;
    }
}