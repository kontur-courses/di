namespace TagCloud.Core.Layouting
{
    public interface ILayouterFactoryResolver
    {
        ILayouterFactory Get(TagCloudLayouterType type);
    }
}