using System;


namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordsReader = new WordsReader();
            var wordStorage = wordsReader.ReadWords("..\\..\\text.txt", new WordStorage(new WordsCustomizer()));
            var words = wordStorage.ToSortedList();

            Console.WriteLine();
        }
    }
}
