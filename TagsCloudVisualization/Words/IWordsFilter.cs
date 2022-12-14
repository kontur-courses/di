namespace TagsCloudVisualization.Words;

public interface IWordsFilter
{
    Dictionary<string, int> FilterWords(Dictionary<string, int> wordsAndCount, VisualizationOptions options);
}