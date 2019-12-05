using System.Drawing;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizer.CloudViewConfiguration
{
    public interface IColorWordPicker
    {
        Color GetColor(Word word);
    }
}
