namespace TagsCloudGenerator.Interfaces
{
    public interface IPriorityExecutable<TIn, TOut>
    {
        TOut Execute(TIn input);
        int Priority { get; }
    }
}