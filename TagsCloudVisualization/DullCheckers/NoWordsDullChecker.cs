namespace TagsCloudVisualization;

public class NoWordsDullChecker : IDullWordChecker
{
    public bool Check(string word)
    {
        return false;
    }
}