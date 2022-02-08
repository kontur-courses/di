namespace TagsCloudContainer.Processing;

public interface IWordsProcessor
{
    bool TryProcess(string word, out string processedWord);
}