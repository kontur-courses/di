using System.Collections.Generic;
using System.Drawing;
using TagCloud.Extensions;
using TagCloud.Tags;

namespace TagCloud
{
    public class TagCloud : ITagCloud
    {
        public Point Center { get; }

        public List<ITag> Layouts { get; }

        public TagCloud(Point center)
        {
            Center = center;

            Layouts = new List<ITag>();
        }

        public int GetWidth() =>
            Layouts.MaxOrDefault(r => r.Frame.Right) - 
            Layouts.MinOrDefault(r => r.Frame.Left);

        public int GetHeight() =>
            Layouts.MaxOrDefault(r => r.Frame.Bottom) - 
            Layouts.MinOrDefault(r => r.Frame.Top);

        public int GetLeftBound() => Layouts.MinOrDefault(r => r.Frame.Left);

        public int GetTopBound() => Layouts.MinOrDefault(r => r.Frame.Top);
    }
}
