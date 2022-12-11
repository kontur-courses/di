using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;

public interface IWordsPreprocessor
{
    ISet<IWord> GetProcessedWords(List<string> words, IReadOnlyCollection<IBoringWords> boringWords);
}