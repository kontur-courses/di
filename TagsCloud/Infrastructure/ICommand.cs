namespace TagsCloud.Infrastructure
{
    public interface ICommand
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Execute(string[] args);
    }
}