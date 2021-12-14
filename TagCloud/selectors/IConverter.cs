namespace TagCloud.selectors
{
    public interface IConverter<T>
    {
        T Convert(T source);
    }
}