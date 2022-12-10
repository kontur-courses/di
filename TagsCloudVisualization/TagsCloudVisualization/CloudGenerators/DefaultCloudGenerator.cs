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

    public List<TextLabel> GenerateCloud(string text)
    {
        var rects = new List<TextLabel>();
        var wordFreq = Preprocessor.Preprocessing(text);
        var g = Graphics.FromImage(new Bitmap(1, 1));
        var generalCount = wordFreq.Values.Sum();
        foreach (var (word, freq) in wordFreq.OrderByDescending(pair => pair.Value))
        {
            Font font = new Font("Arial", (int)(16f * (1 + freq * 1f / generalCount)));
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