using System.Drawing;
using System.Windows.Forms;
using TagsCloudLayouter;

namespace TagCloud;

public class CloudDrawer : ICloudDrawer
{
    public Palette Palette { get; set; }
    public ICloudLayouter Layouter { get; set; }
    public SizeProperties Size { get; set; }
    public ITextToTagsConverter TextToTagsConverter { get; set; }
    public FontProperties FontProperties { get; set; }


    public CloudDrawer(ITextToTagsConverter textToTagsConverter, SizeProperties size, ICloudLayouter layouter,
        Palette palette, FontProperties fontProperties)
    {
        Layouter = layouter;
        Palette = palette;
        TextToTagsConverter = textToTagsConverter;
        FontProperties = fontProperties;
        Size = size;
    }

    public Bitmap Draw()
    {
        var words = ComputeTags(TextToTagsConverter.GetTags());

        var bitmap = new Bitmap(Size.ImageSize.Width, Size.ImageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        using var backgroundBrush = new SolidBrush(Palette.Background);
        graphics.FillRectangle(backgroundBrush, new Rectangle(new Point(0, 0), Size.ImageSize));

        using var brush = new SolidBrush(Palette.Foreground);
        foreach (var textBox in words)
            graphics.DrawString(textBox.Text, textBox.Font, brush, textBox.Location);

        return bitmap;
    }

    private IEnumerable<TextBox> ComputeTags(Dictionary<string, int> tags)
    {
        var maxCount = tags.Values.Max();
        foreach (var tag in tags)
        {
            var text = new TextBox();
            text.Text = tag.Key;
            text.ForeColor = Palette.Foreground;
            text.TextAlign = FontProperties.TextAlign;
            var size = ComputeFontSize(tag.Value, maxCount, FontProperties.MinSize, FontProperties.MaxSize);
            text.Font = new Font(FontProperties.Family, size, FontProperties.Style);
            text.Location = Layouter.PutNextRectangle(text.PreferredSize).Location;
            yield return text;
        }

        Layouter.Clear();
    }

    private static float ComputeFontSize(float count, float maxCount, float minSize, float maxSize) =>
        minSize + count / maxCount * (maxSize - minSize);
}