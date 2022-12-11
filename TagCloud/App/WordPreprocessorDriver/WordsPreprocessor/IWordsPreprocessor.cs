using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor;

/// <summary>
/// Интерфейс, предоставляющий возможность обработки слов текста
/// </summary>
public interface IWordsPreprocessor
{
    /// <summary>
    /// Позволяет получить обработанные неповторяющиеся слова с посчитанным индексом tf
    /// </summary>
    /// <param name="words">Список всех слов</param>
    /// <param name="boringWords">Сущность, которая позволит определять, скучное ли слово</param>
    /// <returns>Обработанные неповторяющиеся слова</returns>
    ISet<IWord> GetProcessedWords(List<string> words, IReadOnlyCollection<IBoringWords> boringWords);
}