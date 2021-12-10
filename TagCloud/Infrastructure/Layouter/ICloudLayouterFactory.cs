namespace TagCloud.Infrastructure.Layouter;

public interface ICloudLayouterFactory
{
    ICloudLayouter Create(string layouterName);
}