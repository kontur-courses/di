namespace TagsCloudContainer.Interfaces;

public interface IPreprocessor
{
    IEnumerable<string> Preprocess(IEnumerable<string> words);
}
