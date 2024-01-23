namespace TagsCloudVisualization;

public class LoremDullWordChecker : IDullWordChecker
{
    public bool Check(string word)
    {
        var dullWords = new HashSet<string>() { "in", "eu", "sed" };
        return dullWords.Contains(word);
    }
}