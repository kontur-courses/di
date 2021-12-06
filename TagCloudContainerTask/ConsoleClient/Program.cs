using System.Linq;
using TagCloud.Words.Filtering;
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

            var wordPreprocessor = new WordPreprocessor();
            var wordsToLower = wordPreprocessor.LeadingWordsToLower(words);

            var wordsFilter = new WordsFilter();
            var filteredWords = wordsFilter.FilterWords(wordsToLower);
        }
    }
}