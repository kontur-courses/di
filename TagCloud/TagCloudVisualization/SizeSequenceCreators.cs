using System;
using System.Drawing;

namespace TagCloudVisualization
{
    public static class SizeSequenceCreators
    {
        public static CreatorInfo Random => new CreatorInfo(RandomCreator, 200, 150);
        public static CreatorInfo SlowDecreasing => new CreatorInfo(SlowDecreasingCreator, 50, 25);
        public static CreatorInfo Decreasing => new CreatorInfo(DecreasingCreator, 200, 100);

        private static Size SlowDecreasingCreator(int n, int i) => new Size((125 - n + i) % 125 + 1, n % 50 + 1);

        private static Size RandomCreator(int n, int i)
        {
            var random = new Random(42);
            return new Size(random.Next(10, 200), random.Next(10, 200));
        }

        private static Size DecreasingCreator(int n, int i) => new Size(Math.Abs(300 - n), Math.Abs(300 - n));

        public class CreatorInfo
        {
            public CreatorInfo(Func<int, int, Size> creator, int start, int count)
            {
                Creator = creator;
                Start = start;
                Count = count;
            }

            public Func<int, int, Size> Creator { get; }

            public int Start { get; }

            public int Count { get; }
        }
    }
}
