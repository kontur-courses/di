using System;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class Vector
    {
        public readonly int X;
        public readonly int Y;

        public Vector(int length, double angle)
        {
            X = (int)(length * Math.Sin(angle));
            Y = (int)(length * Math.Cos(angle));
        }
    }
}