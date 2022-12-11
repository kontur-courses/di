namespace TagsCloud2;

public class Reader: IReader
{
    public List<string> ReadWordsFromFile(string pathToFile)
    {
        var words = File.ReadAllLines(pathToFile);
        return new List<string>(words);
    }
}