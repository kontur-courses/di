using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.FontFactory
{
    public interface IFontFactory
    {
        FontDecorator GetFont(Word word, int minCount, int maxCount);
    }
}