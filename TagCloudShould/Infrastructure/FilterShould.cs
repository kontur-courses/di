using TagCloudContainer.Filters;

namespace TagCloudShould.Infrastructure
{
    [TestFixture]
    public class FilterShould
    {
        [Test]
        public void Filter_WhenHaveBoredWords()
        {
            var filter = new FilterWords();
            var arrayString = new[] { "Good", "Morning", "u", "like", "me" };
            arrayString = filter.Filter(arrayString, x => x.Length > 3).ToArray();
            arrayString.Length.Should().Be(3);
        }
        [Test]
        public void Filter_WhenHaveWordsWithDigits()
        {
            var filter = new FilterWords();
            var arrayString = new[] { "Good", "Morning", "3434", "343", "meee" };
            arrayString = filter.Filter(arrayString, x => !int.TryParse(x, out var result)).ToArray();
            arrayString.Length.Should().Be(3);
        }
    }
}
