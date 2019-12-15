using System.Drawing;
using CircularCloudLayouter;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TagsCloudForm;
using TagsCloudForm.Actions;
using TagsCloudForm.CircularCloudLayouterSettings;
using TagsCloudForm.CloudPainters;
using TagsCloudForm.Common;

namespace TagsCloudTests.PainterTests
{



    [TestClass]
    public class CloudPainterTests
    {

        private IImageHolder imageHolder;
        private ICircularCloudLayouterSettings settings;
        private IPalette palette;
        private ICircularCloudLayouter layouter;
        private IGraphicDrawer graphics;

        [SetUp]
        public void SetUp()
        {
            imageHolder = A.Fake<IImageHolder>();
            settings = A.Fake<ICircularCloudLayouterSettings>();
            palette = A.Fake<IPalette>();
            layouter = A.Fake<ICircularCloudLayouter>();
            graphics = A.Fake<IGraphicDrawer>();
            A.CallTo(() => imageHolder.GetImageSize()).Returns(new Size(600, 600));
            A.CallTo(() => imageHolder.StartDrawing()).Returns(graphics);
            A.CallTo(() => settings.MinSize).Returns(10);
            A.CallTo(() => settings.MaxSize).Returns(30);
            A.CallTo(() => settings.CenterX).Returns(300);
            A.CallTo(() => settings.CenterY).Returns(300);
            A.CallTo(() => settings.IterationsCount).Returns(5);
            A.CallTo(() => settings.XCompression).Returns(1);
            A.CallTo(() => settings.YCompression).Returns(1);
            A.CallTo(() => palette.PrimaryColor).Returns(Color.Black);
            A.CallTo(() => palette.BackgroundColor).Returns(Color.Black);
            A.CallTo(() => palette.SecondaryColor).Returns(Color.Black);
        }


        [Test]
        public void CloudPainter_ShouldCallLayouterExactTimesThatSpecifiedInSettings()
        {
            var iterations = 10;
            A.CallTo(() => settings.IterationsCount).Returns(iterations);
            var painter = new CloudPainter(imageHolder, settings, palette, layouter);

            painter.Paint();

            A.CallTo(() => layouter.PutNextRectangle(new Size(10,10))).WithAnyArguments().MustHaveHappened(iterations, Times.Exactly);
        }

        [Test]
        public void CloudPainter_ShouldCallUpdate_OnlyOnce()
        {
            var painter = new CloudPainter(imageHolder, settings, palette, layouter);

            painter.Paint();

            A.CallTo(() => imageHolder.UpdateUi()).WithAnyArguments().MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CloudPainter_ShouldCallDrawRectangle_ExactTimesThatSpecifiedInSettings()
        {
            var iterations = 10;
            A.CallTo(() => settings.IterationsCount).Returns(iterations);
            var painter = new CloudPainter(imageHolder, settings, palette, layouter);

            painter.Paint();

            A.CallTo(() => graphics.DrawRectangle(default, default)).WithAnyArguments().MustHaveHappened(iterations, Times.Exactly);
        }
    }
}
