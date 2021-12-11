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
        IColoredCloud GetFromCloudLayouter(string[] words, ICloudLayouter cloud, IColoringAlgorithm coloringAlgorithm, Font font);

        List<IColoredSizedWord> GetColoredWords();
    }
}
