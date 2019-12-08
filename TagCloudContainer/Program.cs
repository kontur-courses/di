using TagCloudContainer.fluent;

namespace TagCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = "words.txt";
            var outputFile = "wordCloud.bmp";

            CreateTagCloud.FromFile(inputFile).SaveToFile(outputFile);
        }
    }
}