using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloudTests
{
	[TestFixture]
	public class ProcessingSourceWordsToLayout_Test
	{
		private LayoutConstructor layoutConstructor;
		private ITextReader textReader;
		private string resourcesDirectory;

		[OneTimeSetUp]
		public void SetUp()
		{
			resourcesDirectory = TestContext.CurrentContext.TestDirectory + @"\..\..\Resources\";
			
			textReader = Substitute.For<ITextReader>();
			var filters = new[] {new WordLengthFilter()};
			var wordsProcessor = new WordsProcessor(textReader, filters);
			var fontSettings = new FontSettings();
			var imageHolder = new PictureBoxImageHolder();
			imageHolder.RecreateImage(new ImageSettings());
			var tagsProcessor = new TagsProcessor(wordsProcessor, fontSettings, imageHolder);
			var spiralParameters = new SpiralParameters();
			var spiral = new ArchimedeSpiral(spiralParameters);
			var layouter = new CircularCloudLayouter(spiral);
			layoutConstructor = new LayoutConstructor(tagsProcessor, layouter);
		}

		[Test]
		public void LayoutIsCorrect()
		{
			var path = resourcesDirectory + "ProcessingSourceWordsToLayout_Test.txt";
			textReader.Read().Returns(File.ReadAllLines(path));
			var expectedLayoutTags = new[]
			{
				new Tag("hell", 27, new Rectangle(0, 0, 105, 36)), 
				new Tag("aaa", 12, new Rectangle(-1, 16, 36, 16)), 
				new Tag("bbb", 12, new Rectangle(-4, 32, 36, 16)), 
				new Tag("cccc", 10, new Rectangle(1, -36, 38, 13)), 
				new Tag("bbbb", 10, new Rectangle(35, 15, 38, 13)), 
			};

			var actualLayoutTags = layoutConstructor.GetLayout().Tags;
			actualLayoutTags.Should().BeEquivalentTo(expectedLayoutTags);
		}
	}
}