namespace TagsCloudVisualization;

public class NoWordsDullChecker : IDullWordChecker
{
    public bool Check(WordAnalysis word)
    {
        return false;
    }
}