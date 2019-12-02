namespace TagsCloudGenerator.Interfaces
{
    internal interface IExecutor<TIn, TOut>
    {
        TOut Execute(TIn input);
    }
}