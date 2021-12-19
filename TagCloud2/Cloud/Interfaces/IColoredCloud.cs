using System.Collections.Generic;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public interface IColoredCloud
    {
        List<IColoredSizedWord> ColoredWords { get; }

        void AddColoredWordsFromCloudLayouter(IColoredSizedWord[] words, ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm);
    }
}
