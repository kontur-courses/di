using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Visualizers
{
    public class AlternatingTagColoring : ITagColoring
    {
        private IEnumerator<Color> colorEnumerator;

        public AlternatingTagColoring(IEnumerable<Color> colors)
        {
            colorEnumerator = colors.ToList().GetEnumerator();
        }

        public Color GetNextColor()
        { 
            if (colorEnumerator.MoveNext()) 
                return colorEnumerator.Current;
            colorEnumerator.Reset();
            colorEnumerator.MoveNext();
            return colorEnumerator.Current;
        }
    }
}
