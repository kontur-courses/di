using System.Drawing;
using TagsCloudContainer.Data;

namespace TagsCloudContainer.Visualization.Measurers
{
    public interface IWordMeasurer
    {
        (Font font, Size size) Measure(Word word);
    }
}