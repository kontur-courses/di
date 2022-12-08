namespace TagsCloudVisualization.Words;

public interface IWordsPreprocessing
{
    IEnumerable<string> ProcessWords(IEnumerable<string> rawWords);
}