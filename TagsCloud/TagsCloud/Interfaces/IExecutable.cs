namespace TagsCloudGenerator.Interfaces
{
    public interface IExecutable<TIn, TOut>
    {
        TOut Execute(TIn input);
    }
}