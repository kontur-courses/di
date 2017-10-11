using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer
{
    [TestFixture]
    class SimpleWordPreprocessor_Schould
    {
        private SimpleWordPreprocessor simpleWordPreprocessor;
        private IWordFormater wordFormater1;
        private IWordFormater wordFormater2;

        private string[] strings = new string[] { "a", "M", "Ф", "к" };

        [SetUp]
        public void SetUp()
        {
            wordFormater1 = Substitute.For<IWordFormater>();
            wordFormater2 = Substitute.For<IWordFormater>();

            simpleWordPreprocessor = new SimpleWordPreprocessor(new IWordFormater[] { wordFormater1, wordFormater2 });
        }

        [Test]
        public void DoingNothingWithArray_IfArrayFormatersNull()
        {
            simpleWordPreprocessor = new SimpleWordPreprocessor(null);

            simpleWordPreprocessor.Handle(strings).Should().BeEquivalentTo(strings);
        }

        [Test]
        public void DoingNothingWithArray_IfArrayFormatersCountEquallyZero()
        {
            simpleWordPreprocessor = new SimpleWordPreprocessor(new IWordFormater[] { });

            simpleWordPreprocessor.Handle(strings).Should().BeEquivalentTo(strings);
        }

        [Test]
        public void ArrayFormaters_IsCalling()
        {
            simpleWordPreprocessor.Handle(strings);

            wordFormater1.Received().HandleWords(Arg.Any<string[]>());
            wordFormater2.Received().HandleWords(Arg.Any<string[]>());
        }
    }
}
