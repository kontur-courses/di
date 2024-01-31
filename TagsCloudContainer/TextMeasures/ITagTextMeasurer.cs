
using System.Drawing;

namespace TagsCloudContainer.TextMeasures;

public interface ITagTextMeasurer
{
    public Size Measure(string text);
}