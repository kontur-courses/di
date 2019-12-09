using System.Drawing;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloudTests
{
	[TestFixture]
	public class LayoutConstructor_Tests
	{
		private ITagsProcessor tagsProcessor;
		private ICloudLayouter layouter;
		private LayoutConstructor layoutConstructor;

		[SetUp]
		public void SetUp()
		{
			tagsProcessor = Substitute.For<ITagsProcessor>();
			layouter = Substitute.For<ICloudLayouter>();
			layoutConstructor = new LayoutConstructor(tagsProcessor, layouter);
		}

		[Test]
		public void GetLayout_CreatesNewLayoutAfterEachCall()
		{
			layouter.PlaceNextRectangle(Size.Empty).Returns(Rectangle.Empty);

			var firstLayout = layoutConstructor.GetLayout();
			tagsProcessor.GetTags().Returns(new Tag[5]);
			var secondLayout = layoutConstructor.GetLayout();

			secondLayout.Tags.Should().HaveCount(5);
			firstLayout.Should().NotBe(secondLayout);
		}
	}
}