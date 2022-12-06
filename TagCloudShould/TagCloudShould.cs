using Autofac;
using TagCloudContainer.Filters;
using TagCloudContainer.Formatters;
using TagCloudContainer.FrequencyWords;
using TagCloudContainer.Parsers;
using TagCloudContainer.PointAlgorithm;
using TagCloudContainer.Readers;
using TagCloudContainer.Rectangles;
using TagCloudContainer.TagsWithFont;

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
            circularLayouter = new CircularCloudLayouter();
            var reader = new TxtReader();
            var text=reader.Read((Environment.CurrentDirectory +
                                   "\\..\\..\\..\\word_data\\data.txt"));
            var words = new FileLinesParser().Parse(text);
            tagCloud = InitializeCloud(words);
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
           // tagCloud.Save(pathDebug);
            Console.WriteLine("Tag cloud visualization saved to file: " + pathDebug);
        }


        private TagCloud InitializeCloud(IEnumerable<string> rowWords)
        {
            var filterWords = new FilterWords();
            var filtredTags = filterWords.Filter(rowWords);
            var formatter = new WordFormatter();
            var formattedTags = formatter.Normalize(filtredTags, x => x.ToLower());
            var freqtag = new FrequencyTags();
            var freqTags = freqtag.GetWordsFrequency(formattedTags);
            var fontSizer = new FontSizer();
            var fontTags = fontSizer.GetTagsWithSize(freqTags, new FontFamily("Times"), 150, 50);
            return new TagCloud(fontTags);
        }



        [Test]
        public void NoVisualization_NoIntersections()
        {
            tagCloud.CreateTagCloud(circularLayouter, new ArithmeticSpiral(Point.Empty));
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
            tagCloud.CreateTagCloud(circularLayouter, new ArithmeticSpiral(Point.Empty));
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
        
            tagCloud.CreateTagCloud(circularLayouter, new ArithmeticSpiral(Point.Empty));
        }

        [Test]
        public void Visualization_WithRandomSave()
        {
            var random = new Random(123);
            var strsplt = new string[400];
            for (var i = 0; i < 400; i++)
                strsplt[i] = random.Next(1, 100).ToString();
            tagCloud=InitializeCloud(strsplt);
            tagCloud.CreateTagCloud(circularLayouter, new ArithmeticSpiral(Point.Empty));
        }
    }
}
