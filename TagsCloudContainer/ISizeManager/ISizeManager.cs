using System.Drawing;

namespace TagsCloudVisualization;

public interface ISizeManager
{
    public Dictionary<string, Rectangle> GetSizesForWords(Size fieldSize, Dictionary<string, double> freqWords,
        int wordsCount);
}