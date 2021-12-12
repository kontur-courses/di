namespace TagsCloudContainer
{
    public interface IResolver<TKey, TService>
    {
        public TService Get(TKey key);
    }
}