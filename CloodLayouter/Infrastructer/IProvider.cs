namespace CloodLayouter.Infrastructer
{
    public interface IProvider<Type>
    {
        Type Get();
    }
}