using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud2;
using TagCloud2.Text;

namespace TagCloud2Tests
{
    public class StringPreprocessorTests
    {
        private StringPreprocessor sp = new StringPreprocessor(new SillyWordsRemover(), new ShortWordsSelector()); 

        [Test]
        public void PreprocessString_OnWord_MustReturnLowercase()
        {
            sp.PreprocessString("СТраННое").Should().Be("странное");
        }

        [Test]
        public void PreprocessString_OnBoringWord_MustReturnEmptyString()
        {
            sp.PreprocessString("sad").Should().Be("");
        }
    }
}
