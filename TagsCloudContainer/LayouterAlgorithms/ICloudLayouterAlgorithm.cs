using System.Drawing;

namespace TagsCloudContainer.LayouterAlgorithms
{
    public interface ICloudLayouterAlgorithm
    {
        public Point PlaceNextWord(string word, int wordCount, int coefficient);
    }
}