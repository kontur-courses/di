namespace TagsCloudContainer
{
    public interface IFactory<T>
    {
        public T Create();
    }
}