using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Core;

namespace TagsCloudVisualization.Layouters
{
    public interface IWordSizeChooser
    {
        Dictionary<Word, Size> GetWordSizes(Word[] words, int wordHeight = 1, float letterToWidthRatio = 1);
    }
}