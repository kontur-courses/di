namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            LayouterVisualizer.CreateCloudWithWordsFromFile("words.txt", "words.bmp");
        }
    }
}