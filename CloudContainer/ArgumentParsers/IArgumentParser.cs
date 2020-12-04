namespace CloudContainer.ArgumentParsers
{
    public interface IArgumentParser
    {
        Arguments Parse(string[] args);
    }
}