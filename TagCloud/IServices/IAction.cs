namespace TagCloud
{
    public interface IAction
    {
        void Perform();
        string CommandName { get; }
    }
}
