using System.Collections.Generic;
using TagsCloudVisualisation.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.WordsPreprocessor
{
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
        ISet<Word> GetProcessedWords(List<string> words, params IBoringWords[] boringWords);
    }
}