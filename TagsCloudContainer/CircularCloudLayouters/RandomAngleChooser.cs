using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class RandomAngleChooser : IAngleChooser
    {
        private readonly Queue<double> angles = new Queue<double>();
        private readonly Random random;
        private readonly double deltaAngleStep;
        private const double PI = Math.PI;
        

        public RandomAngleChooser(Random random)
        {
            deltaAngleStep = PI / 3;
            this.random = random;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public bool MoveNext()
        {
            if (!angles.Any())
               AddNewAngles();
            Current = angles.Dequeue();
            return true;
        }

        public void Reset()
        {
            angles.Clear();
        }

        public double Current { get; private set; }

        object IEnumerator.Current => Current;

        private void AddNewAngles()
        {
            var angle = GetRandomAngle();
            var countSteps = (int)(2 * PI / deltaAngleStep);
            for (var indexStep = 0; indexStep < countSteps; indexStep++)
                angles.Enqueue(angle + deltaAngleStep * indexStep);
        }

        private double GetRandomAngle()
            => 2 * random.NextDouble() * PI;

    }
}