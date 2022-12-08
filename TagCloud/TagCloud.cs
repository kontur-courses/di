using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Tags;

namespace TagCloud
{
    public class TagCloud
    {
        public Point Center { get; private set; }

        public List<ITag> Layouts { get; private set; }

        public TagCloud(Point center)
        {
            Center = center;

            Layouts = new List<ITag>();
        }

        public int GetWidth()
        {
            if (Layouts.Count == 0)
                return 0;

            return Layouts.Max(r => r.Frame.Right) - 
                Layouts.Min(r => r.Frame.Left);
        }

        public int GetHeight()
        {
            if (Layouts.Count == 0)
                return 0;

            return Layouts.Max(r => r.Frame.Bottom) - 
                Layouts.Min(r => r.Frame.Top);
        }

        public int GetLeftBound() => Layouts.Min(r => r.Frame.Left);

        public int GetTopBound() => Layouts.Min(r => r.Frame.Top);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj) => Equals(obj as TagCloud);

        public bool Equals(TagCloud other)
        {
            return other != null &&
                   other.Center == Center &&
                   other.Layouts.Count == Layouts.Count &&
                   other.Layouts.TrueForAll(rectangle => 
                       this.Layouts.Contains(rectangle));
        }
    }
}
