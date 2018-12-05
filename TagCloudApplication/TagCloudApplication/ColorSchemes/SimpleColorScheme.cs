using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication.ColorSchemes
{
    public class SimpleColorScheme : IColorScheme
    {
        public Color BackColor { get; } = Color.White;
        public Color GetNextColorForWord() => Color.Black;
    }
}
