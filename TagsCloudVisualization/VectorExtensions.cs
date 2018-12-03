using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class VectorExtensions
    {
        public static double ScalarMultiply(this Vector currentVector, Vector otherVector)
        {
            var x = currentVector.Position.X * otherVector.Position.X;
            var y = currentVector.Position.Y * otherVector.Position.Y;

            return x + y;
        }

        public static bool IsSameDirection(this Vector currentVector, Vector otherVector)
        {
            if (currentVector == otherVector)
                return true;
            return currentVector.ScalarMultiply(otherVector) > 0;
        }

    }
}
