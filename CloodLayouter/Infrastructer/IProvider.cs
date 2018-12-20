namespace CloodLayouter.Infrastructer
{
    public interface IProvider<T>
    {
        T Get();
    }
}