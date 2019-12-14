using System.Drawing;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace
{
    class DefaultColorPicker : IColorWordPicker
    {
        public Color GetColor(Word word)
        {
            return Color.White;
        }
    }
}
