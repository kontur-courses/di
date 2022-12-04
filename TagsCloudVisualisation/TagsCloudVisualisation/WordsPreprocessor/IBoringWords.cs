﻿namespace TagsCloudVisualisation.WordsPreprocessor
{
    /// <summary>
    /// Интерфейс, который позволяет проверять слова на "скучность"
    /// </summary>
    public interface IBoringWords
    {
        /// <summary>
        /// Метод, который проверяет, является ли слова скучным, не нужным
        /// </summary>
        /// <param name="word">Слово для проверки</param>
        /// <returns>true - если слово скучное и подлежит исключению. false - в противном случае</returns>
        bool IsBoring(Word word);
    }
}