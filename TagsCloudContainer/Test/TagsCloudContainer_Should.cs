using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace TagsCloudContainer
{
    [TestFixture]
    class TagsCloudContainer_Should
    {
        private ICircularCloudLayouter circularCloudLoyauter;
        private ITagsData tagsData;
        private TagsCloudContainer tagsCloudConteiner;
        private ITagSizeNormalizer tagSizeNormalizer;

        [SetUp]
        public void SetUp()
        {
            tagsData = Substitute.For<ITagsData>();
            circularCloudLoyauter = Substitute.For<ICircularCloudLayouter>();
            tagSizeNormalizer = Substitute.For<ITagSizeNormalizer>();


            tagsCloudConteiner = new TagsCloudContainer(tagsData, circularCloudLoyauter, tagSizeNormalizer);
        }


        [Test]
        public void TagsRectangleData_IsEmpty()
        {
            tagsCloudConteiner.GetTagsRectangleData().Should().BeEmpty();
        }


        [Test]
        public void TagsData_CallGetdata()
        {
            tagsCloudConteiner.GetTagsRectangleData();

            tagsData.Received().GetData();
        }

        [Test]
        public void TagsRectangleData_ShouldNotBeEpmty()
        {
            var strings = new string[] { "1" };

            tagsData.GetData().Returns(strings);

            tagsCloudConteiner.GetTagsRectangleData().Should().NotBeEmpty();
        }

        [Test]
        public void TagsRectangleData_ShouldHaveEqualsTagsData_Count()
        {
            var strings = new string[] { "1", "2", "3", "4", "5" };
            tagsData.GetData().Returns(strings);

            tagsCloudConteiner.GetTagsRectangleData().Should().HaveCount(5);

        }

        [Test]
        public void CallsOfGetRectanglesEquelsCountOfTagsData()
        {
            var strings = new string[] { "1", "2", "3", "4", "5" };
            var counter = 0;
            tagsData.GetData().Returns(strings);
            circularCloudLoyauter
                .PutNextRectangle(Arg.Any<Size>())
                .ReturnsForAnyArgs(x => new Rectangle())
                .AndDoes(x => counter++);

            tagsCloudConteiner.GetTagsRectangleData();

            counter.Should().Be(5);
        }


        [Test]
        public void CallsOfNormalizerEquelsCountOfTagsData()
        {
            var strings = new string[] { "1", "2", "3", "4", "5" };
            var counter = 0;
            tagsData.GetData().Returns(strings);
            tagSizeNormalizer
                .GetTagSize(Arg.Any<string>())
                .ReturnsForAnyArgs(x => new Size())
                .AndDoes(x => counter++);

            tagsCloudConteiner.GetTagsRectangleData();

            counter.Should().Be(5);
        }
    }
}
