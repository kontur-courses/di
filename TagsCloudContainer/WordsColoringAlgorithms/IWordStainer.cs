using System.Drawing;

namespace TagsCloudContainer.WordsColoringAlgorithms
{

    public interface IWordStainer
    {
        public Color[] GetColorsSequence();
    }
}