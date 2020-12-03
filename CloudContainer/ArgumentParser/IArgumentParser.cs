namespace CloudContainer.ArgumentParser
{
    public interface IArgumentParser
    {
        Arguments Parse(string[] args);
    }
}