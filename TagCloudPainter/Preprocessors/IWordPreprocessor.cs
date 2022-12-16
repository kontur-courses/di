namespace TagCloudPainter.Preprocessors;

public interface IWordPreprocessor
{
    Dictionary<string, int> GetWordsCountDictionary(IEnumerable<string> words);
}