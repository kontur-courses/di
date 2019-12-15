using System.Collections.Generic;
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
    public class CloudWithWordsPainterTests
    {
        private IImageHolder imageHolder;
        private ICircularCloudLayouterWithWordsSettings settings;
        private IPalette palette;
        private ICircularCloudLayouter layouter;
        private IGraphicDrawer graphics;

        [SetUp]
        public void SetUp()
        {
            imageHolder = A.Fake<IImageHolder>();
            settings = A.Fake<ICircularCloudLayouterWithWordsSettings>();
            palette = A.Fake<IPalette>();
            layouter = A.Fake<ICircularCloudLayouter>();
            graphics = A.Fake<IGraphicDrawer>();
            A.CallTo(() => imageHolder.GetImageSize()).Returns(new Size(600, 600));
            A.CallTo(() => imageHolder.StartDrawing()).Returns(graphics);
            A.CallTo(() => settings.CenterX).Returns(300);
            A.CallTo(() => settings.Scale).Returns(5);
            A.CallTo(() => settings.CenterY).Returns(300);
            A.CallTo(() => palette.PrimaryColor).Returns(Color.Black);
            A.CallTo(() => palette.BackgroundColor).Returns(Color.Black);
            A.CallTo(() => palette.SecondaryColor).Returns(Color.Black);
        }


        [Test]
        public void CloudPainter_ShouldCallLayouterExactTimesThatSpecifiedInSettings()
        {
            var words = new Dictionary<string, int> {["hello"]=1};
            var painter = new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);

            painter.Paint();

            A.CallTo(() => layouter.PutNextRectangle(new Size(10, 10))).WithAnyArguments().MustHaveHappened(words.Count, Times.Exactly);
        }

        [Test]
        public void CloudPainter_ShouldCallUpdate_OnlyOnce()
        {
            var words = new Dictionary<string, int> { ["hello"] = 1 };
            var painter = new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);

            painter.Paint();

            A.CallTo(() => imageHolder.UpdateUi()).WithAnyArguments().MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CloudPainter_ShouldCallStartDrawing_OnlyOnce()
        {
            var words = new Dictionary<string, int> { ["hello"] = 1 };
            var painter = new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);

            painter.Paint();

            A.CallTo(() => imageHolder.StartDrawing()).WithAnyArguments().MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CloudPainter_ShouldCallDrawRectangle_AsMuchAsWordsCount()
        {
            var words = new Dictionary<string, int>
            {
                ["hello"] = 1,
                ["hell"] = 2,
                ["hel"] = 3,
                ["he"] = 4

            };
            A.CallTo(() => settings.Frame).Returns(true);
            var painter = new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);

            painter.Paint();

            A.CallTo(() => graphics.DrawRectangle(default, default)).WithAnyArguments().MustHaveHappened(words.Count, Times.Exactly);
        }

        [Test]
        public void CloudPainter_ShouldCallDrawString_AsMuchAsWordsCount()
        {
            var words = new Dictionary<string, int>
            {
                ["hello"] = 1,
                ["hell"] = 2,
                ["hel"] = 3,
                ["he"] = 4

            };
            var painter = new CloudWithWordsPainter(imageHolder, settings, palette, layouter, words);

            painter.Paint();

            A.CallTo(() => graphics.DrawString(default, default, default, default)).WithAnyArguments().MustHaveHappened(words.Count, Times.Exactly);
        }
    }
}
