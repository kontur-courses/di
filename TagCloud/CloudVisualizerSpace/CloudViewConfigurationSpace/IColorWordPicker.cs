using System.Drawing;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace
{
    public interface IColorWordPicker
    {
        Color GetColor(Word word);
    }
}
