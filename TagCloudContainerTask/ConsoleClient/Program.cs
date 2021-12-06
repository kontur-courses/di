using TagCloud.WordsPreprocessing;

namespace ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var pp = new InitialWordFormReader();
            var words = pp.ReadWordsFromFile(@"C:/users/sqire/desktop/ss.txt");
        }
    }
}