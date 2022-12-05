using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TagsCloudVisualisation.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.WordsPreprocessor
{
    /// <summary>
    /// Класс, предоставляющий возможности базовой обработки слов.
    /// Приводит все слова к нижнему регистру, подсчитывает их количество и tf индекс, изюавляется от "скучных" слов,
    /// таких как предлоги, союзы, местоимения
    /// </summary>
    public class DefaultWordsPreprocessor : IWordsPreprocessor
    {
        private readonly CultureInfo cultureInfo;
        private HashSet<Word> wordsSet;

        public DefaultWordsPreprocessor(CultureInfo cultureInfo)
        {
            this.cultureInfo = cultureInfo;
            wordsSet = new HashSet<Word>();
        }

        /// <summary>
        /// Позволяет получить обработанные неповторяющиеся слова с посчитанным индексом tf
        /// </summary>
        /// <param name="words">Список всех слов</param>
        /// <param name="boringWords">Сущность, которая позволит определять, скучное ли слово</param>
        /// <returns>Обработанные неповторяющиеся слова без скучных слов</returns>
        public ISet<Word> GetProcessedWords(List<string> words, params IBoringWords[] boringWords)
        {
            wordsSet = CreateWordsSet(words, word => word.ToLower(cultureInfo));
            CalculateTfIndexes(wordsSet, words.Count);
            return wordsSet
                .Where(word => 
                    boringWords.All(checker => !checker.IsBoring(word)))
                .ToHashSet();
        }

        /// <summary>
        /// Метод, который позволяет получить tf индекс
        /// </summary>
        /// <param name="wordCount">Количество раз, которое слова встречается в текста</param>
        /// <param name="totalWordsCount">Общее количество слов в тексте</param>
        /// <returns>Значение tf индекса</returns>
        private static double GetTfIndex(int wordCount, int totalWordsCount)
        {
            return 1d * totalWordsCount / wordCount;
        }
        
        /// <summary>
        /// Метод, который зааполняет свойства tf у каждого слова
        /// </summary>
        /// <param name="words">Список неповторяющихся слов текста</param>
        /// <param name="totalWordsCount">Общее количество слов в текста</param>
        private static void CalculateTfIndexes(IEnumerable<Word> words, int totalWordsCount)
        {
            foreach (var word in words)
            {
                word.Tf = GetTfIndex(word.Count, totalWordsCount);
            }
        }
        
        /// <summary>
        /// Метод, который аозволяет сформировать набор уникальных слов текста, применив к каждому слову некоторый
        /// алгоритм предварительной обработки (приведение к начальной форме, к lowercase и прочие...)
        /// </summary>
        /// <param name="words">Все слова в тексте</param>
        /// <param name="wordPreprocessor">Алгоритм предвартельного форматирования каждого слова</param>
        /// <returns>Set уникальных слов в тексте</returns>
        private static HashSet<Word> CreateWordsSet(IEnumerable<string> words, Func<string, string> wordPreprocessor)
        {
            var set = new HashSet<Word>();
            foreach (var word in words.Select(wordValue => new Word(wordPreprocessor(wordValue))))
            {
                if (set.TryGetValue(word, out var containedWord))
                    containedWord.Count++;
                else 
                    set.Add(word);
            }

            return set;
        }
    }
}