using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer;


public class Program
{

    public static void Main(string[] args)
    {
        var cfg = new AppConfig();
        var freqWords = cfg.InputParser.GetInputWordsFrequency();
        var wordsToSizes = GetSizesForWords(cfg.FieldSize, freqWords);
        var layoutDrawer = new LayoutDrawer(new Pen(Color.Black, 2));
        layoutDrawer.Draw(wordsToSizes, "TagCloud.png");
    }

    private static Dictionary<string, Rectangle> GetSizesForWords(Size fieldSize, Dictionary<string, double> freqWords)
    {
        var result = new Dictionary<string, Rectangle>();
        var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), new ArchimedeanSpiral(1, 1, 0));
        foreach (var key in freqWords.Keys)
        {
            var rectSize = new Size((int)(freqWords[key] * fieldSize.Width), (int)(freqWords[key] * fieldSize.Height));
            var rectangle = circularCloudLayouter.PutNextRectangle(rectSize);
            result[key] = rectangle;
        }

        return result;
    }
}