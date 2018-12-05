using TagsCloudVisualization;
using NUnit.Framework;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class MathHelper_Should
    {
        [TestCase(1, 2, ExpectedResult = 2, TestName = "Positive values")]
        [TestCase(1, -2, ExpectedResult = -2, TestName = "Different sign values")]
        [TestCase(-1, -2, ExpectedResult = -2, TestName = "Negative values")]
        public int MaxAbs_ReturnCorrectVal(int val1, int val2) => MathHelper.MaxAbs(val1, val2);
    }
}