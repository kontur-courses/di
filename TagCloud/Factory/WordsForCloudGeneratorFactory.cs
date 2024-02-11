using TagCloud.Interfaces;
using TagsCloudVisualization;
namespace TagCloud.Factory;

public class WordsForCloudGeneratorFactory : IWordsForCloudGeneratorFactory
{
    public ITagGenerator Get(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
        IColorGenerator colorGenerator)
    {
        return new TagGenerator(fontName, maxFontSize, tagCloudLayouter, colorGenerator);
    }
}