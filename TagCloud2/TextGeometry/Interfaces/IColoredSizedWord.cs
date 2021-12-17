using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.TextGeometry
{
    public interface IColoredSizedWord
    {
        Rectangle Size { get; }

        Color Color { get; }

        string Word { get; }

        Font Font { get; }
    }
}
