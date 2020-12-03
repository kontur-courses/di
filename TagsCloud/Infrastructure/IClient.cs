namespace TagsCloud.Infrastructure
{
    public interface IClient
    {
        string[] GetAvailableCommandName();
        void Run();
        ICommand FindCommandByName(string name);
    }
}