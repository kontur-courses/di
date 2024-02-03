using System.Drawing;

namespace TagsCloud.WordFontCalculators;

public interface IWordFontCalculator
{
    public Font GetWordFont(string word, int count);
}