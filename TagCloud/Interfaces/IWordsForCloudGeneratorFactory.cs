using TagCloud.Interfaces;
using TagsCloudVisualization;
namespace TagCloud;

public interface IWordsForCloudGeneratorFactory
{
    ITagGenerator Get(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
        IColorGenerator colorGenerator);
}