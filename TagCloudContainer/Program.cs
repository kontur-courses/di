using System.Drawing;

namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            LayouterVisualizer.CreateCloudWithWordsFromFile("words.txt", new Font("ComicSans", 8), "words.bmp");
        }
    }
}