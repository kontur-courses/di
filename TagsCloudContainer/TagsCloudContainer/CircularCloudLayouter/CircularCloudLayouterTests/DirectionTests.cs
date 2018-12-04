using NUnit.Framework;


namespace TagsCloudContainer.CircularCloudLayouter.CircularCloudLayouterTests
{
    [TestFixture]
    internal class DirectionTests
    {
        [TestCase(ExpectedResult = 0, TestName = "BeZeroWhenTakeFirstValue")]
        public double DirectionShould()
        {
            return new Direction().GetNextDirection();
        }

        [TestCase(ExpectedResult = 0.01, TestName = "BeOneOnSecondStep")]
        public double AlphaShould()
        {
            var direction = new Direction();
            direction.GetNextDirection();

            return direction.GetNextDirection();
            ;
        }
    }
}