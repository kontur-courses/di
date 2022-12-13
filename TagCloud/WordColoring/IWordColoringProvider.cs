namespace TagCloud.WordColoring
{
    public interface IWordColoringProvider
    {
        IWordColoring GetWordColoringByName(string name);
    }
}
