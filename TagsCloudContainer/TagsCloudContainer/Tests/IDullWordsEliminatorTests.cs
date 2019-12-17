using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer
{
    [TestFixture]
    public class IDullWordsEliminatorTests
    {
        [Test]
        public void NothingDullEliminator_AllWordsAreNotDull()
        {
            var words = new string[] {"I", "begin", "doing", "this", "work"};

            var eliminator = new NothingDullEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeFalse();
        }

        [Test]
        public void OnlyNounDullWordsEliminator_SkipsNotNounWords()
        {
            var words = new string[] { "I", "begin", "doing", "this"};

            var eliminator = new OnlyNounDullWordsEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeTrue();
        }

        [Test]
        public void OnlyNounDullWordsEliminator_DoesNotSkipNouns()
        {
            var words = new string[] { "Coin", "Coil", "Pen", "Python" };

            var eliminator = new OnlyNounDullWordsEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeFalse();
        }

        [Test]
        public void DefaultDullWordsEliminator_SkipsPronouns()
        {
            var words = new string[] { "I", "he", "she", "they", "we",
                "me", "her", "his", "them", "us" };

            var eliminator = new DefaultDullWordsEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeTrue();
        }

        [Test]
        public void DefaultDullWordsEliminator_SkipsPrepositions()
        {
            var words = new string[] { "at", "by", "on", "in", "with" };

            var eliminator = new DefaultDullWordsEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeTrue();
        }

        [Test]
        public void DefaultDullWordsEliminator_DoesNotSkipVerbsAndNouns()
        {
            var words = new string[] {"Man", "stopped", "running", "marathon"}; 

            var eliminator = new DefaultDullWordsEliminator();

            foreach (var word in words)
                eliminator.IsDull(word).Should().BeFalse();
        }
    }
}