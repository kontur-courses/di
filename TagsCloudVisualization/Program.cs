using System.Drawing;
namespace TagsCloudVisualization;

public static class Program
{
    public static void Main()
    {
        var filePath = $@"{AppDomain.CurrentDomain.BaseDirectory}TextSamples\";
        var wordParser = new WordParser();
        var words = wordParser.GetAllWords($"{filePath}LoremTest.txt");
        var wordsCount = new Dictionary<string, int>();
        
        
        foreach (var word in words)
        {
            if (!wordsCount.TryAdd(word, 1)) wordsCount[word] += 1;
        }

        var circularCloudLayouter = new CloudLayouter(new SpiralGenerator(), new Font("Arial", 40), wordsCount);
        LayoutDrawer.CreateLayoutImage(circularCloudLayouter.CreateLayout(), "TestPenIs1");
    }
}