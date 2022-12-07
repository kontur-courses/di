using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public class TagCloud
    {
        public Point Center { get; private set; }

        public List<Rectangle> Rectangles { get; private set; }

        public TagCloud(Point center)
        {
            Center = center;

            Rectangles = new List<Rectangle>();
        }

        public int GetWidth()
        {
            if (Rectangles.Count == 0)
                return 0;

            return Rectangles.Max(r => r.Right) - 
                Rectangles.Min(r => r.Left);
        }

        public int GetHeight()
        {
            if (Rectangles.Count == 0)
                return 0;

            return Rectangles.Max(r => r.Bottom) - 
                Rectangles.Min(r => r.Top);
        }

        public int GetLeftBound() => Rectangles.Min(r => r.Left);

        public int GetTopBound() => Rectangles.Min(r => r.Top);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj) => Equals(obj as TagCloud);

        public bool Equals(TagCloud other)
        {
            return other != null &&
                   other.Center == Center &&
                   other.Rectangles.Count == Rectangles.Count &&
                   other.Rectangles.TrueForAll(rectangle => 
                       this.Rectangles.Contains(rectangle));
        }
    }
}
