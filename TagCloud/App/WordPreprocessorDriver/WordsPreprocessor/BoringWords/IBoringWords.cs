using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;

public interface IBoringWords
{ 
    bool IsBoring(IWord word);
}