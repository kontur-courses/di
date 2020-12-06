using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Infrastructure.Layout.Environment
{
    public class PlainEnvironment : IEnvironment<Rectangle>
    {
        public PlainEnvironment()
        {
            Elements = new List<Rectangle>();
        }

        private List<Rectangle> Elements { get; }

        public void Add(Rectangle element)
        {
            Elements.Add(element);
        }

        public bool IsColliding(Rectangle element)
        {
            return Elements.Any(placedRectangle => placedRectangle.IntersectsWith(element));
        }

        public IEnumerator<Rectangle> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Elements.Clear();
        }

        public void Remove(Rectangle element)
        {
            Elements.Remove(element);
        }
    }
}