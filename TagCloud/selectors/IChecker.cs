namespace TagCloud.selectors
{
    public interface IChecker<in T>
    {
        bool IsValid(T source);
    }
}