using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Spiral
    {
        private Point? lastGeneratedPoint;
        public Point Center { get; }
        public double Phi { get; private set; }
        public double SpiralEquationParam { get; }
        public double DeltaPhi { get; }

        public Spiral(Point center, double spiralEquationParam = 1, double deltaPhi = Math.PI / 180)
        {
            ValidateParam(spiralEquationParam);

            Center = center;
            Phi = 0;
            SpiralEquationParam = spiralEquationParam;
            DeltaPhi = deltaPhi;
        }

        public Point GetNextPoint()
        {
            Point newPoint;

            do
            {
                var x = Math.Round(SpiralEquationParam * Phi * Math.Cos(Phi)) + Center.X;
                var y = Math.Round(SpiralEquationParam * Phi * Math.Sin(Phi)) + Center.Y;
                Phi += DeltaPhi;

                newPoint = new Point((int)x, (int)y);
            } while (newPoint == lastGeneratedPoint);

            lastGeneratedPoint = newPoint;
            return newPoint;
        }

        private static void ValidateParam(double spiralParam)
        {
            if (spiralParam <= 0)
                throw new ArgumentException("Spiral param must be greater than zero");
        }
    }
}