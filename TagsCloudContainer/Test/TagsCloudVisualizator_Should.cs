using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TagsCloudContainer.Test
{

    [TestFixture]
    class TagsCloudVisualizator_Should
    {
        private ITagsCloudContainer tagsCloudContainer;
        private IColorPicker randomColorPiker;
        private IColorPicker whiteColorPicker;
        private Point center;
        private Font font;
        private TagsCloudVisualizator tagsCloudVisualizator;

        [SetUp]
        public void SetUp()
        {
            tagsCloudContainer = Substitute.For<ITagsCloudContainer>();
            randomColorPiker = Substitute.For<IColorPicker>();
            whiteColorPicker = Substitute.For<IColorPicker>();
            center = new Point(1000, 1000);
            font = new Font(FontFamily.GenericMonospace, 16, FontStyle.Italic, GraphicsUnit.Point);

            tagsCloudVisualizator = new TagsCloudVisualizator(tagsCloudContainer, new IColorPicker[] { randomColorPiker, whiteColorPicker }, center, font);

            randomColorPiker.GenerateColor().Returns(new SolidBrush(Color.FromArgb(21, 213, 222)));
            whiteColorPicker.GenerateColor().Returns(Brushes.White);
            tagsCloudContainer.GetTagsRectangleData().Returns(new Dictionary<string, Rectangle>()
            {
                {"dasda", new Rectangle()},
                {"das21da", new Rectangle()},
            });
        }

        [Test]
        public void GetTagsCloudImage_ReturnBitmap()
        {
            tagsCloudVisualizator.GetTagsCloudImage().Should().BeOfType<Bitmap>();
        }

        [Test]
        public void GetTagsCloudImage_CallGetTagsRectangleData()
        {
            tagsCloudVisualizator.GetTagsCloudImage();

            tagsCloudContainer.Received().GetTagsRectangleData();
        }

        [Test]
        public void GetTagsCloudImage_CallGenerateColor()
        {
            tagsCloudVisualizator.GetTagsCloudImage();

            randomColorPiker.Received().GenerateColor();
            whiteColorPicker.Received().GenerateColor();
        }

        [Test]
        public void GetTagsCloudImage_CallGenerateColorSomeTimes()
        {
            tagsCloudVisualizator.GetTagsCloudImage();

            randomColorPiker.Received(2).GenerateColor();
            whiteColorPicker.Received(2).GenerateColor();
        }
    }
}

