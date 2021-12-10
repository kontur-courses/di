namespace TagsCloudContainer.DependencyInjection
{
    public interface IServiceResolver<TType, TService> where TService : IService<TType> where TType : notnull
    {
        public TService GetService(TType type);
    }
}