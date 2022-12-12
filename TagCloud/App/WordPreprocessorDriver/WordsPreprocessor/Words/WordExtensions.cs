using System.Drawing;

namespace TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

public static class WordExtensions
{
    public static Size MeasureWord(this IWord word, Font font)
    {
        using var bitmap = new Bitmap(1, 1);
        using var graphics = Graphics.FromImage(bitmap);
        var result = graphics.MeasureString(word.Value, font);
        if (result.Width < 1) result.Width = 1;
        if (result.Height < 1) result.Height = 1;
        return result.ToSize();
    }
}