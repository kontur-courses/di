using System.Linq;
using TagCloud.WordsPreprocessing;

namespace ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var wordsReader = new InitialWordFormReader();
            //TODO: Подумать, можно ли исправить поведение myStem с кириллицей?
            var testInputFile = @"C:/users/sqire/desktop/words.txt";
            var words = wordsReader.ReadWordsFromFile(testInputFile).ToList();
        }
    }
}