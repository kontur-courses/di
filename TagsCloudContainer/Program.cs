using System;
using System.Linq;

namespace TagsCloudContainer
{
    class Program
    {
        public static void Main()
        {
            var path = "C:\\Users\\vas21\\Desktop\\wp.txt";
            var text = new TxtReader().ReadFromFile(path);
            var words = new TextParser().GetWords(text);
            var validWords = new SimplePreprocessor().GetValidWords(words).Take(5);

            foreach (var word in validWords)
            {
                Console.WriteLine(word);
            }
        }
    }
}
