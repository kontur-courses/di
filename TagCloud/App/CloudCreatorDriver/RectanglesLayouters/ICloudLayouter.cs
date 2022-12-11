using System.Drawing;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloud.App.CloudCreatorDriver.RectanglesLayouters;

public interface ICloudLayouter
{
    List<Rectangle> GetLaidRectangles(IEnumerable<Size> sizes, ICloudLayouterSettings layouterSettings);
}