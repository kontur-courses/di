using System.Linq;
using TagCloud.Words.Filtering;
using TagCloud.Words.FrequencyAnalyzing;
using TagCloud.Words.Preprocessing;
using TagCloud.Words.Reading;

namespace ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //TODO: Подумать, можно ли исправить поведение myStem с кириллицей?
            var testInputFile = @"C:/users/sqire/desktop/words.txt";

            var wordsReader = new InitialWordFormReader();
            var words = wordsReader.ReadWordsFromFile(testInputFile).ToList();

            var wordPreprocessor = new ToLowerCasePreprocessor();
            var wordsToLower = wordPreprocessor.Preprocess(words);

            var wordsFilter = new WordsFilter();
            var filteredWords = wordsFilter.FilterWords(wordsToLower);

            var frequencyAnalyzer = new WordsFrequencyAnalyzer();
            var wordsFrequencies = frequencyAnalyzer.AnalyzeWordsFrequency(filteredWords);
        }
    }
}