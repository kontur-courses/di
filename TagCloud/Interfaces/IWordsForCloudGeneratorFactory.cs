using TagsCloudVisualization;
namespace TagCloud;

public interface IWordsForCloudGeneratorFactory
{
    IWordsForCloudGenerator Get(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
        IColorGenerator colorGenerator);
}