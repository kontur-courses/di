namespace TagCloud
{
    public interface IAction
    {
        string CommandName { get; }
        void Perform(ClientConfig config);
    }
}