namespace TagsCloudContainer.DependencyInjection
{
    public interface IService<TType> where TType : notnull
    {
        TType Type { get; }
    }
}