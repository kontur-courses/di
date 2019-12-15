using FakeItEasy;
using NUnit.Framework;
using TagsCloudForm.Actions;
using TagsCloudForm.CloudPainters;

namespace TagsCloudTests.ActionTests
{
    [TestFixture]
    public class LayouterWithWordsActionTests
    {
        private IPainterFactory painterFactory;
        private ICloudPainter painter;

        [SetUp]
        public void SetUp()
        {
            painterFactory = A.Fake<IPainterFactory>();
            painter = A.Fake<ICloudPainter>();
            A.CallTo(() => painterFactory.Create()).Returns(painter);
        }

        [Test]
        public void CircularCloudLayouterAction_CallsPaint_WhenPerformCalled()
        {
            var action = new CircularCloudLayouterWithWordsAction(painterFactory);

            action.Perform();

            A.CallTo(() => painter.Paint()).MustHaveHappened();
        }

        [Test]
        public void CircularCloudLayouterAction_CallsPaintOnlyOnce_WhenPerformCalled()
        {
            var action = new CircularCloudLayouterWithWordsAction(painterFactory);

            action.Perform();

            A.CallTo(() => painter.Paint()).MustHaveHappenedOnceExactly();
        }
    }
}
