using System;
using System.Drawing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using NUnit;
using NUnit.Framework;
using TagsCloudForm;
using FluentAssertions;
using TagsCloudForm.Common;

namespace TagsCloudTests
{
    [TestFixture]
    public class PictureBoxImageHolderTests
    {
        [Test]
        public void PictureBox_Startdrawing_ShouldThrowExcepionWhenNoPicture()
        {
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.Invoking(x => x.StartDrawing()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PictureBox_GetImageSize_ShouldThrowExcepionWhenNoPicture()
        {
            var pictureBox = new PictureBoxImageHolder();

            pictureBox.Invoking(x => x.GetImageSize()).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void PictureBox_SaveImage_ShouldThrowExcepionWhenNoPicture()
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
