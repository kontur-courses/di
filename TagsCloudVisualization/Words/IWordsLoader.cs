namespace TagsCloudVisualization.Words;

public interface IWordsLoader
{
    IEnumerable<Word> LoadWords(VisualizationOptions options);
}