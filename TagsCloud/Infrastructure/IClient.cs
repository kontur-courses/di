namespace TagsCloud.Infrastructure
{
    public interface IClient
    {
        string[] GetAvailableCommandName();
        void Run();
    }
}