using System;
using System.Collections.Generic;

namespace TagCloudVisualization
{
    public abstract class AbstractSpiralGenerator
    {
        private protected IEnumerator<Point> Enumerator;

        public bool IsStarted => Enumerator != null;

        private protected Point Center { get; private set; }

        public AbstractSpiralGenerator Begin(Point center)
        {
            if (Enumerator != null)
                throw new InvalidOperationException("Begin method must be called only once");

            Center = center;
            Enumerator = GetEnumerator();
            Enumerator.MoveNext();
            return this;
        }

        private protected abstract IEnumerator<Point> GetEnumerator();

        public Point Next()
        {
            if (Enumerator == null)
                throw new InvalidOperationException("One must call Begin method before usage");
            Enumerator.MoveNext();
            return Enumerator.Current;
        }
    }
}
