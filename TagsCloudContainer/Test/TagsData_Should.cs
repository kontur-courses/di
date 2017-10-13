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
    class TagsData_Should
    {
        private IFileParser fileParser;
        private IWordPreprocessor wordPreprocessor;
        private TagsData tagsData;
        private readonly string[] words = new string[] { "Ольга", "Нина", "Альбина" };

        [SetUp]
        public void SetUp()
        {
            fileParser = Substitute.For<IFileParser>();
            wordPreprocessor = Substitute.For<IWordPreprocessor>();
            tagsData = new TagsData(fileParser, wordPreprocessor);
        }

        [Test]
        public void ReturnData_WhenCallGetData()
        {
            fileParser.ReadLinesToArray().Returns(words);
            wordPreprocessor.Handle(Arg.Any<string[]>()).Returns(new string[] { "ольга", "нина", "альбина" });

            tagsData.GetData().Should().Equal(new string[] { "ольга", "нина", "альбина" });
        }

        [Test]
        public void FileParser_CallReadAllLineToArray()
        {
            tagsData.GetData();

            fileParser.Received().ReadLinesToArray();
        }

        [Test]
        public void IWordPreprocessor_CallHandle()
        {
            fileParser.ReadLinesToArray().Returns(words);

            tagsData.GetData();

            wordPreprocessor.Received().Handle(words);
        }

        [Test]
        public void OrderOfCalls_GetData()
        {
            tagsData.GetData();

            Received.InOrder(() =>
            {
                fileParser.ReadLinesToArray();
                wordPreprocessor.Handle(Arg.Any<IEnumerable<string>>());
            });
        }
    }
}
