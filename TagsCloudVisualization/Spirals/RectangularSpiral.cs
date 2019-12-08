using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class RectangularSpiral : ISpiral
    {
        static Size[] directions = new Size[]
        {
            new Size(0, 1),
            new Size(1, 0),
            new Size(0, -1),
            new Size(-1, 0),
        };

        public IEnumerable<Point> GetPoints()
        {
            var stepNumber = 0;
            var curentPosition = new Point(0, 0);
            yield return curentPosition;

            while (true)
            {
                var countStepsInCurentDirection = (stepNumber / 2) + 1;
                var curentDirection = directions[stepNumber % 4];
                for (int i = 0; i < countStepsInCurentDirection; i++)
                {
                    curentPosition += curentDirection;
                    yield return curentPosition;
                }
                stepNumber += 1;
            }
        }
    }
}
