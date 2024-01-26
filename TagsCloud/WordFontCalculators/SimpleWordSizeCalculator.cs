using System.Drawing;

namespace TagsCloud.WordSizeCalculators;

public class SimpleWordSizeCalculator: IWordSizeCalculator
{
    public Font GetWordFont(string word, int count)
    {
        return new Font("Arial", count);
    }
}