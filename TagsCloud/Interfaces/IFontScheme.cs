using System.Drawing;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFontScheme
    {
        Font Process(FrequentedWord element);
    }
}