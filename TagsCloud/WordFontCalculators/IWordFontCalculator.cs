using System.Drawing;

namespace TagsCloud.WordSizeCalculators;

public interface IWordFontCalculator
{
    
    public Font GetWordFont(string word, int count);
}