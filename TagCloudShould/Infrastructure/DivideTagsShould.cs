namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class DivideTagsShould
    {

        [TestCase(1, 1, TestName = "Equals")]
        [TestCase(2, 1, TestName = "Minimal size upper maximum")]
        [TestCase(0, 1, TestName = "ZeroSize")]
        [TestCase(1, -1, TestName = "NegativeSize")]
        [TestCase(int.MinValue, 1, TestName = "MinIntSize")]

        public void ThrowException_WhenMinSizeUpperOrEqualMaxSie(int minSize, int maxSize)
        {
            Action createTags = () => new FrequencyTags()
                .GetDictionaryWithTags(new[] { "1", "2", "3", "4" })
                .DivideTags(maxSize, minSize);
            createTags.Should().Throw<ArgumentNullException>();
        }

        [TestCase(30, 15, TestName = "Even number")]
        [TestCase(int.MaxValue, 1073741823, TestName = "Max int number")]
        [TestCase(5, 2, TestName = "Odd number")]
        public void CheckTagSize_WhenHaveOnlySize(int tagSize, int expectedResult)
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "3", "4" });
            var dividedTags = frequencyTags.DivideTags(tagSize, tagSize / 2);
            foreach (var pair in dividedTags)
                pair.Value.Should().Be(expectedResult);
        }


        [Test]
        public void DivideTags_WhenDifferentSizeTags_CheckCount()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "2" });
            var dividedTags = frequencyTags.DivideTags(2, 1);
            dividedTags.Values.Count.Should().Be(2);
        }

        [Test]
        public void DivideTags_WhenDifferentSizeTags_CheckSize()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "2" });
            var dividedTags = frequencyTags.DivideTags(120, 60);
            dividedTags["2"].Should().Be(dividedTags["1"] * 2);
        }


        [Test]
        public void DivideTags_CheckForBigSize()
        {
            var frequencyTags = new FrequencyTags().GetDictionaryWithTags(new[] { "1", "2", "3", "4" });
            var size = int.MaxValue;
            var dividedTags = frequencyTags.DivideTags(size, size / 2);
            foreach (var pair in dividedTags)
                pair.Value.Should().Be(1073741823);
        }
    }
}
