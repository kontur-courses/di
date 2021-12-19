using FluentAssertions;
using NUnit.Framework;
using TagCloud2;
using TagCloud2.Text;

namespace TagCloud2Tests
{
    public class StringPreprocessorTests
    {
        private readonly StringPreprocessor sp = new(new SillyWordsFilter(), new ShortWordsSelector()); 

        [Test]
        public void PreprocessString_OnWord_MustReturnLowercase()
        {
            sp.PreprocessString("СТраННое").Should().Be("СТраННое");
        }

        [Test]
        public void PreprocessString_OnBoringWord_MustReturnEmptyString()
        {
            sp.PreprocessString("sad").Should().Be("");
        }
    }
}
