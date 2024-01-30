using System.Drawing;
using TagsCloudContainer.Cloud;

namespace TagsCloudContainer.WordProcessing;

public class WordRectangleGenerator
{
    public static List<Rectangle> GenerateRectangles(List<Word> words, CircularCloudLayouter layouter,
        Settings settings)
    {
        return words.Select(word =>
        {
            var font = new Font(settings.FontName, word.Size);
            var size = EditingWordSize(word.Value, font);
            return layouter.PutNextRectangle(size);
        }).ToList();
    }

    private static Size EditingWordSize(string word, Font font)
    {
        var bitmap = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(bitmap);
        var result = graphics.MeasureString(word, font).ToSize();
        // var result = graphics.MeasureString(word, font);
        // result = result.ToSize();
        if (result.Width == 0) result.Width = 1;
        if (result.Height == 0) result.Height = 1;
        return result; //.ToSize();
    }
}