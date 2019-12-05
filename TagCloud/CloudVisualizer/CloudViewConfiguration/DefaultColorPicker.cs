using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizer.CloudViewConfiguration
{
    class DefaultColorPicker : IColorWordPicker
    {
        public Color GetColor(Word word)
        {
            return Color.White;
        }
    }
}
