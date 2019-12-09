namespace TagsCloudGenerator.Interfaces
{
    public interface IFactory<TResult>
        where TResult : IFactorial
    {
        TResult CreateSingle();
        TResult[] CreateArray();
    }
}