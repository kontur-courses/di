using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudForm;
using TagsCloudForm.Common;

namespace TagsCloudTests
{
    [TestFixture]
    public class PictureBoxImageHolderTests
    {
        [Test]
        public void PictureBox_StartDrawing_ShouldThrowExceptionWhenNoPicture()
        {
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.Invoking(x => x.StartDrawing()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PictureBox_GetImageSize_ShouldThrowExceptionWhenNoPicture()
        {
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.Invoking(x => x.GetImageSize()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PictureBox_SaveImage_ShouldThrowExceptionWhenNoPicture()
        {
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.Invoking(x => x.SaveImage("test.png")).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PictureBox_RecreateImage_ShouldUseSizeFromInput()
        {
            var size = new Size(300, 300);
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.RecreateImage(new ImageSettings {Width = size.Width, Height = size.Height});

            pictureBox.GetImageSize().Should().Be(size);
        }
    }
}
