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

    public Dictionary<string, Rectangle> GenerateCloud(string text)
    {
        var rects = new Dictionary<string, Rectangle>();
        var wordFreq = Preprocessor.Preprocessing(text);
        foreach (var (word, freq) in wordFreq)
        {
            //rects.Add(word, new Rectangle(0,0,));
        }

        return rects;
    }
}