namespace TagsCloudVisualization.MorphAnalyzer;

public interface IMorphAnalyzer
{
    Dictionary<string, WordMorphInfo> GetWordsMorphInfo(IEnumerable<string> words);
}