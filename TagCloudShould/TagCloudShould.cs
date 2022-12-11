using TagCloudContainer;
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
        private ICloudCreateSettings settings;
        private IEnumerable<ITag> defaultTags;

        [SetUp]
        public void SetUp()
        {
            tagCloud = new TagCloud();
            settings = new CloudCreateSettings(new ArithmeticSpiral() { Config = new PointConfig(1, 1) },
                new RectangleBuilder());
            var reader = new Reader();
            var text = reader.TxtRead((Environment.CurrentDirectory +
                                       "\\..\\..\\..\\word_data\\data.txt"));
            var words = new FileLinesParser().Parse(text);
            defaultTags = InitializeTags(words);
        }


        private IEnumerable<ITag> InitializeTags(IEnumerable<string> rowWords)
        {
            var filterWords = new FilterWords();
            var filtredTags = filterWords.Filter(rowWords, x => x.Length > 0);
            var formatter = new WordFormatter();
            var formattedTags = formatter.Normalize(filtredTags, x => x.ToLower());
            var freqtag = new FrequencyTags();
            var freqTags = freqtag.GetWordsFrequency(formattedTags);
            var fontSizer = new FontSizer();
            var fontTags = fontSizer.GetTagsWithSize(freqTags,
                new FontSettings() { Font = new FontFamily("Times"), MaxFont = 150, MinFont = 50 });
            return fontTags;
        }



        [Test]
        public void CreateCloudDefaultTags_NoIntersections()
        {
            tagCloud.CreateTagCloud(settings, defaultTags);
            var rectangles = tagCloud.GetRectangles();
            foreach (var rectangle in rectangles)
                foreach (var thisRectangle in rectangles.Where(rect => rect != rectangle))
                    thisRectangle.rectangle.IntersectsWith(rectangle.rectangle).Should().BeFalse();
        }

        [Test]
        public void CreateCloudDefaultTags_NoArgumentExceptionWhenNotSpaceForSmallestTag()
        {
            Action runVisual = () => ArgumentException_WhenHaveEmptySpaceSmall();
            runVisual.Should().NotThrow<Exception>();
        }

        public void ArgumentException_WhenHaveEmptySpaceSmall()
        {
            tagCloud.CreateTagCloud(settings, defaultTags);
            var arithmeticSpiral = new ArithmeticSpiral() { Config = new PointConfig(1, 1) };
            var point = arithmeticSpiral.GetNextPoint();
            var rectangles = tagCloud.GetRectangles();
            var smallRectangle = rectangles.Last().rectangle;
            var smallOptions = new Tuple<string, Size, Font>("small", smallRectangle.Size, new Font("Times", 5));
            while (!new Rectangle(point - smallOptions.Item2 / 2, smallOptions.Item2).IntersectsWith(smallRectangle))
            {
                point = arithmeticSpiral.GetNextPoint();
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
        public void CreateCloudDefaultTags_Timeout()
        {

            tagCloud.CreateTagCloud(settings, defaultTags);
        }

        [Test, Timeout(5000)]
        public void CreateCloud_WithRandomNumbers()
        {
            var random = new Random(123);
            var strsplt = new string[400];
            for (var i = 0; i < 400; i++)
                strsplt[i] = random.Next(1, 100).ToString();
            var tags = InitializeTags(strsplt);
            tagCloud.CreateTagCloud(settings, tags);
        }
    }
}
