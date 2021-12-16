using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud2.TextGeometry;
using TagCloudVisualisation;

namespace TagCloud2
{
    public interface IColoredCloud
    {
        IColoredCloud GetFromCloudLayouter(IColoredSizedWord[] words, ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm);

        List<IColoredSizedWord> GetColoredWords();
    }
}
