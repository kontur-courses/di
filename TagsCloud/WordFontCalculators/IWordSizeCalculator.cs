using System.Drawing;

namespace TagsCloud.WordSizeCalculators;

public interface IWordSizeCalculator
{
    public Font GetWordFont(string word, int count);
}