namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

public interface IWord : IEquatable<IWord>
{
    string Value { get; }
    int Count { get; set; }
    double Tf { get; set; }
}