using System.Drawing;

namespace TagsCloudVisualization;

public class DefaultCloudGenerator : ICloudGenerator
{
    public CircularCloudLayouter Layouter { get; set; }
    public IPreprocessor Preprocessor { get; set; }

    public DefaultCloudGenerator(CircularCloudLayouter layouter, IPreprocessor preprocessor)
    {
        Layouter = layouter;
        Preprocessor = preprocessor;
    }

    public Dictionary<string, Point> GenerateCloud(string text)
    {
        var rects = new Dictionary<string, Point>();
        var wordFreq = Preprocessor.Preprocessing(text);
        foreach (var (word, freq) in wordFreq)
        {
            //TODO: реализовать генерацию облака с помощью Layouter
        }

        return rects;
    }
}