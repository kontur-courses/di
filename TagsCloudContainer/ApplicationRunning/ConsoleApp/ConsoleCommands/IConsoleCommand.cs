namespace TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands
{
    public interface IConsoleCommand
    {
        void Act(string[] args);
        string Name { get; }
        string Description { get; }
        string Arguments { get; }
    }
}