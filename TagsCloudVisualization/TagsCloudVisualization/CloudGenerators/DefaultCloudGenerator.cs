using System.Drawing;

namespace TagsCloudVisualization;

public class DefaultCloudGenerator : ICloudGenerator
{
    public CircularCloudLayouter Layouter { get; set; }
    public IPreprocessor Preprocessor { get; set; }
    private Font font;

    public DefaultCloudGenerator(CircularCloudLayouter layouter, IPreprocessor preprocessor, Font font)
    {
        Layouter = layouter;
        Preprocessor = preprocessor;
        this.font = font;
    }

    public List<TextLabel> GenerateCloud(string text)
    {
        var rects = new List<TextLabel>();
        var wordFreq = Preprocessor.Preprocessing(text);
        var g = Graphics.FromImage(new Bitmap(1, 1));
        var generalCount = wordFreq.Values.Sum();
        foreach (var (word, freq) in wordFreq.OrderByDescending(pair => pair.Value))
        {
            SizeF sz = g.MeasureString(word, font);
            Rectangle rc = new Rectangle(0, 0, (int)sz.Width, (int)sz.Height);

            rc = Layouter.PutNextRectangle(rc.Size);

            rects.Add(new TextLabel()
            {
                Position = new Point(rc.X, rc.Y),
                Content = word,
                Font = font
            });
        }

        return rects;
    }
}