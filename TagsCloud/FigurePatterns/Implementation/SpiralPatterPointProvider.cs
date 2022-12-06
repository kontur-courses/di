using System.Drawing;

namespace TagsCloud.FigurePatterns.Implementation
{
    public class SpiralPatterPointProvider : IFigurePatternPointProvider
    {
        public double Step
        {
            get => step;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Most to be greater then zero", nameof(Step));
                step = value;
            }
        }

        private double step;
        private Point center;
        private double angle;
        private readonly IEnumerator<Point> enumerator;

        public SpiralPatterPointProvider(Point center, double step)
        {
            this.center = center;
            Step = step;
            angle = 0;
            enumerator = Order().GetEnumerator();
        }
        
        public Point GetNextPoint()
        {
            enumerator.MoveNext();
            return enumerator.Current;
        }

        public void Restart()
        {
            angle = 0;
        }
        
        private IEnumerable<Point> Order()
        {
            while (true)
            {
                yield return new Point
                {
                    X = (int) (step / (Math.PI * 2) * angle * Math.Cos(angle) + center.X),
                    Y = (int) (step / (Math.PI * 2) * angle * Math.Sin(angle) + center.Y),
                };
                angle++;
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}