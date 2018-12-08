using System;
using System.Collections;
using System.Collections.Generic;

namespace TagCloudVisualization
{
    public abstract class AbstractSpiralGenerator
    {
        private protected IEnumerator<Point> Enumerator;

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

        private protected Point Center { get; private set; }
        

        public Point Next()
        {
            if (Enumerator == null)
                throw new InvalidOperationException("One must call Begin method before usage");
            Enumerator.MoveNext();
            return Enumerator.Current;
        }

        

    }
    
}