using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface IWordPalette
    {
        Image GetBackground(Size size);
        void ColorWords(IEnumerable<GraphicWord> words);
    }
}
