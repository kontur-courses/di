using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.CircularCloudLayouters
{
    public class RandomAngleChooser : IAngleChooser
    {
        private readonly Stack<double> angles = new Stack<double>();
        private readonly Random random;
        private readonly int countCreatedAnglesByOneAngle;
        private const double PI = Math.PI;
        

        public RandomAngleChooser(Random random)
        {
            countCreatedAnglesByOneAngle = 5;
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
            Current = angles.Pop();
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
            for (var index = 0; index < countCreatedAnglesByOneAngle; index++)
                angles.Push(angle + index * PI * 2 / countCreatedAnglesByOneAngle);
        }

        private double GetRandomAngle()
            => 2 * random.NextDouble() * PI * 2;

    }
}