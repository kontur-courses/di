using System.Collections.Generic;
using System.Linq;
using TagCloud.WordsPreprocessing.WordsSelector;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;
using YandexMystem.Wrapper.Models;

namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    /// <summary>
    /// Приводит слова русского языка к начальной форме 
    /// </summary>
    [Name("RussianLanguageAnalyzer")]
    public class RussianLanguageAnalyzer : ITextAnalyzer
    {
        private readonly WordSelector wordSelector;

        private readonly Mysteam mystem;

        public RussianLanguageAnalyzer(WordSelector wordSelector)
        {
            this.wordSelector = wordSelector;
            mystem = new Mysteam();
        }

        public Word[] GetWords(IEnumerable<string> words, int count)
        {
            var counter = 0;

            return words
                .Select(w => mystem.GetWords(w))
                .Where(w =>
                {
                    if (w.Count == 0) return false;
                    counter++;
                    return true;

                })
                .Select(w => ParseWord(w[0]))
                .GroupBy(w => w.Value)
                .Select(w =>
                {
                    var result = w.First();
                    result.Count = w.Count();
                    return result;
                })
                .Where(wordSelector.CanUseThisWord)
                .Take(count)
                .Select(w =>
                {
                    w.Frequency = (double)w.Count / counter;
                    return w;
                })
                .ToArray();
        }

        private static Word ParseWord(WordModel model)
        {
            SpeechPart speechPart;
            switch (model.Lexems[0].GramPart)
            {
                case GramPartsEnum.Verb:
                    speechPart = SpeechPart.Verb;
                    break;
                case GramPartsEnum.Adjective:
                    speechPart = SpeechPart.Adjective;
                    break;
                default:
                    speechPart = SpeechPart.Noun;
                    break;
            }
            return new Word(model.Lexems[0].Lexeme, speechPart);
        }
    }
}
