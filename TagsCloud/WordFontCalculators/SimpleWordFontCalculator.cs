using System.Drawing;
using TagsCloud.ConsoleCommands;

namespace TagsCloud.WordSizeCalculators;

public class SimpleWordFontCalculator : IWordFontCalculator
{
    private readonly string font;

    public SimpleWordFontCalculator(Options options)
    {
        this.font = options.TagsFont;
    }

    public Font GetWordFont(string word, int count)
    {
        return new Font(font, count);
    }
}