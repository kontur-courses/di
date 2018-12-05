using System.Drawing;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    public abstract class TestsHandler : TestFixtureAttribute
    {
        public CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(center: new Point(500, 500));
        }

        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var debugPath = TestContext.CurrentContext.TestDirectory;
            var subfolderName = "";
            var path = "";

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                subfolderName = "FailedTests";
                path = string.Format($@"{debugPath}\{subfolderName}\");
                TestContext.Error.WriteLine($"Tag cloud visualization saved to file {path}{testName}.png");
            }
            else
            {
                subfolderName = "SuccessTests";
                path = $@"{debugPath}\{subfolderName}\";
            }
            
            Drawer.DrawAndSaveRectangles(new Size(1000, 1000), layouter.Rectangles, testName + ".png", path);
        }
    }
}
