using System.Drawing;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization.Interfaces
{
    public interface IFontScheme
    {
        Font Process(FrequentedWord element);
    }
}