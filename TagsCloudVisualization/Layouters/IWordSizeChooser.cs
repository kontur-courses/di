using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.WordStatistics;

namespace TagsCloudVisualization.Layouters
{
    public interface IWordSizeChooser
    {
        Dictionary<Word, Size> GetWordSizes(AnalyzedText analyzedText, int wordHeight = 1,
            float letterToWidthRatio = 1);
    }
}