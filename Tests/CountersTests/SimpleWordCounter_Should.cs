using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;
using TagsCloudVisualization.Extensions;

namespace Tests.CountersTests
{
    [TestFixture]
    public class SimpleWordCounter_Should
    {
        private SimpleWordsCounter counter;
        
        [SetUp]
        public void SetUp()
        {
            counter = new SimpleWordsCounter();
        }

        [Test]
        public void CountSameWord()
        {
            var sb = new StringBuilder();
            100.Times(() => sb.Append("0 ")).ToArray();
            counter.UpdateWith(sb.ToString());
            
            counter.CountedWords.Keys.Should().BeEquivalentTo("0");
            counter.CountedWords.Values.Should().BeEquivalentTo(100);
        }

        [Test]
        public void CountDifferentWords()
        {
            var sb = new StringBuilder();
            100.Times(() =>
            {
                sb.Append("0 ");
                sb.Append("1 ");
                sb.Append("2 ");
                sb.Append("3 ");
            });
            counter.UpdateWith(sb.ToString());
            
            counter.CountedWords.Keys.Should().BeEquivalentTo("0","1","2","3");
            counter.CountedWords.Values.Should().BeEquivalentTo(4.Times(()=>100));
        }

        [Test]
        public void CountManyCalls()
        {
            100.Times(() => counter.UpdateWith("0 0 "));
            
            counter.CountedWords.Keys.Should().BeEquivalentTo("0");
            counter.CountedWords.Values.Should().BeEquivalentTo(200);
        }

        [Test]
        public void MakeTextLower()
        {
            //TODO Get default upper string
            var upper = "QAZWSXEDCRFVTGBYHNUJMIKOLP";
            counter.UpdateWith(upper);

            counter.CountedWords.Keys.Should().BeEquivalentTo(new []{upper.ToLower()});
        }
    }
}