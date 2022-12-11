using System.Collections.Generic;
using System.Drawing;
using TagCloud.Tags;

namespace TagCloud
{
    public interface ITagCloud
    {
        public Point Center { get; }
        public List<ITag> Layouts { get; }
        public int GetWidth();
        public int GetHeight();
        public int GetLeftBound();
        public int GetTopBound();
    }
}
