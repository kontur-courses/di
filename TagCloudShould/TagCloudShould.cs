using Autofac;

namespace TagCloudShould
{
    [TestFixture]
    public class TagCloudShould
    {
        private TagCloud tagCloud;
        private CircularCloudLayouter circularLayouter;

        [SetUp]
        public void SetUp()
        {
            circularLayouter =
                DividerTags.GetCircularCloudLayouter(Environment.CurrentDirectory +
                                                     "\\..\\..\\..\\word_data\\data.txt");
        }

        [TearDown]
        public void PrintPathAndSaveIfTestIsDown()
        {

            var testName = TestContext.CurrentContext.Test.Name ;

            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
            {
                tagCloud.Save(Environment.CurrentDirectory + "\\..\\..\\..\\saved_images\\" + testName + ".png");
                return;
            }

            var pathDebug = Environment.CurrentDirectory + 
                            "\\..\\..\\..\\saved_images\\debugImages\\" + testName + TestContext.CurrentContext.Result.FailCount + ".png";
            tagCloud.Save(pathDebug);
            Console.WriteLine("Tag cloud visualization saved to file: " + pathDebug);
        }

        [Test]
        public void NoVisualization_NoIntersections()
        {
            var tagCloud = new TagCloud
                (new List<Point>(), new List<TextRectangle>(), circularLayouter, new ArithmeticSpiral(Point.Empty));
            var rectangles = tagCloud.GetRectangles();
            foreach (var rectangle in rectangles)
                foreach (var thisRectangle in rectangles.Where(rect => rect != rectangle))
                    thisRectangle.rectangle.IntersectsWith(rectangle.rectangle).Should().BeFalse();
        }

        [Test]
        public void NoVisualization_NoArgumentExceptionWhenNotSpaceForSmallestTag()
        {
            Action runVisual = () => ArgumentException_WhenHaveEmptySpaceSmall();
            runVisual.Should().NotThrow<Exception>();
        }

        public void ArgumentException_WhenHaveEmptySpaceSmall()
        {
            tagCloud = new TagCloud(new List<Point>(), new List<TextRectangle>(), circularLayouter,
                new ArithmeticSpiral(Point.Empty));
            var arithmeticSpiral = new ArithmeticSpiral(new Point(0, 0));
            var point = arithmeticSpiral.GetPoint();
            var rectangles = tagCloud.GetRectangles();
            var smallRectangle = rectangles.Last().rectangle;
            var smallOptions = new Tuple<string, Size, Font>("small", smallRectangle.Size, new Font("Times", 5));
            while (!new Rectangle(point - smallOptions.Item2 / 2, smallOptions.Item2).IntersectsWith(smallRectangle))
            {
                point = arithmeticSpiral.GetPoint();
                if (!rectangles
                        .Select(x =>
                            x.rectangle.IntersectsWith(new Rectangle(point - smallOptions.Item2 / 2,
                                smallOptions.Item2)))
                        .Contains(true))
                {
                    throw new Exception(
                        $"Rectangle {smallOptions.Item2} input in space: {point - smallOptions.Item2 / 2}.But must in {smallRectangle.Location - smallOptions.Item2 / 2}");
                }
            }
        }

        [Test, Timeout(5000)]
        public void Visualization_Timeout()
        {
            tagCloud = new TagCloud(new List<Point>(), new List<TextRectangle>(), circularLayouter,
                new ArithmeticSpiral(Point.Empty));
        }

        [Test]
        public void Visualization_WithRandomSave()
        {
            var random = new Random(123);
            var strsplt = new string[400];
            for (var i = 0; i < 400; i++)
                strsplt[i] = random.Next(1, 100).ToString();
            var divideTags = new FrequencyTags()
                .GetDictionaryWithTags(string.Join(", ", strsplt).Split(", ")).DivideTags();
            var circularLayouterCloud = new CircularCloudLayouter(divideTags);
            tagCloud = new TagCloud(new List<Point>(), new List<TextRectangle>(), circularLayouterCloud,
                new ArithmeticSpiral(Point.Empty));
        }
    }
    [TestFixture]
    public class Should
    {
        private static string rootDirectory = Environment.CurrentDirectory + "\\..\\..\\..\\";
        [Test]
        public void check_simple()
        {
            var file = "data.txt";
            var cloud = CreateTagCloud(new ArithmeticSpiral(new Point(0, 0)), rootDirectory+ "word_data\\" + file);
            cloud.Save(rootDirectory+ "saved_images\\"+TestContext.CurrentContext.Test.Name+".png");
        }

        public static TagCloud CreateTagCloud(ArithmeticSpiral spiral, string path)
        {
            var builder = new ContainerBuilder();
            builder.RegisterTypes(typeof(List<Point>), typeof(List<TextRectangle>), typeof(TagCloud)).AsSelf();
            builder.RegisterType<CircularCloudLayouter>().AsSelf();
            builder.RegisterInstance(DividerTags.GetCircularCloudLayouter(path)).As<CircularCloudLayouter>();
            builder.RegisterInstance(spiral).As<ArithmeticSpiral>();
            var container = builder.Build();
            return container.Resolve<TagCloud>();
        }
    }
}
