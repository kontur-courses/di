public interface IFrequencyDictionaryBuilder<T>
{
    Dictionary<T, int> Build(T[] items);
}
