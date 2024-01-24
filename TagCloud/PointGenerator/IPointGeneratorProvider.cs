namespace TagCloud.PointGenerator;

public interface IPointGeneratorProvider
{
    IPointGenerator CreateGenerator(string generatorName);
}