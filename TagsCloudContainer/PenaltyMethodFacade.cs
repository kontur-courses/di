using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizationMethods;
using EParser;
using NumMath;
using System.Drawing;

namespace TagsCloudVisualization
{
    internal static class PenaltyMethodFacade
    {
        public static Point Calculate(Func function, PointF start, List<Func> equations)
        {
            PenaltyMethods.Epsilon = 1E-2;
            OneDimensionalSearches.Epsilon = 1E-2;
            DescentMethods.Epsilon = 1E-2;

            Vector r = new Vector(Enumerable.Repeat(200.0, equations.Count).ToArray());
            Vector startVector = new Vector(start.X, start.Y);

            PenaltyMethods.PenaltyMethod(function, startVector, r, t => t, t => t / 2.0, _ => 0, equations, new List<Func>(), out _, out _);
            return new Point((int)Math.Round(startVector[0]), (int)Math.Round(startVector[1]));
        }
    }
}