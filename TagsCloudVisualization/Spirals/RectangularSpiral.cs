using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Spirals
{
    public class RectangularSpiral : ISpiral
    {
        private static readonly Size[] Directions = 
        {
            new Size(0, 1),
            new Size(1, 0),
            new Size(0, -1),
            new Size(-1, 0),
        };

        public IEnumerable<Point> GetPoints()
        {
            var stepNumber = 0;
            var currentPosition = new Point(0, 0);
            yield return currentPosition;

            while (true)
            {
                var countStepsInCurentDirection = (stepNumber / 2) + 1;
                var currentDirection = Directions[stepNumber % 4];
                for (int i = 0; i < countStepsInCurentDirection; i++)
                {
                    currentPosition += currentDirection;
                    yield return currentPosition;
                }
                stepNumber += 1;
            }
        }
    }
}
