using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using TagCloud;
using TagCloud.Factory;
using TagsCloudVisualization;

namespace TagCloudTests;

[TestFixture]
public class WordsForCloudGenerator_Should
{
    private TagGenerator tagGenerator;
    private const string FontName = "Arial";
    private const int MaxFontSize = 10;
    private static readonly Point Center = new(500, 500);
    private Color[] colors = { Color.Black };
    private TagCloudCreatorFactory tagCloudCreatorFactory;

    [SetUp]
    public void Setup()
    {
        Directory.SetCurrentDirectory(
            Directory.GetParent(
                Directory.GetParent(
                    Directory.GetParent(
                        TestContext.CurrentContext.TestDirectory).ToString()).ToString()) + "\\testFiles");
        Directory.GetParent(TestContext.CurrentContext.TestDirectory);

        tagCloudCreatorFactory = new TagCloudCreatorFactory(new WordsForCloudGeneratorFactory(),
            new ColorGeneratorFactory(),
            new CloudDrawerFactory(),
            new TagCloudLayouterFactory(),
            new SpiralPointsFactory(),
            new WordsReader(),
            new WordsNormalizer());

        tagGenerator = new TagGenerator(FontName,
            MaxFontSize,
            new CircularCloudLayouter(new SpiralPoints(Center)),
            new ColorGenerator(colors));
    }

    [Test]
    public void NotRepeated_OnRepeatedWords()
    {
        tagGenerator.Generate(new List<string> { "w", "w", "e" }).Count.Should().Be(2);
    }

    [Test]
    public void MostCommonWord_OnFirstPlace()
    {
        tagGenerator.Generate(new List<string> { "w", "w", "e", "e", "e", "c" })[0].Word.Should().Be("e");
    }

    [Test]
    public void FirstWord_OnCenter()
    {
        var wordForCloud = tagGenerator.Generate(new List<string> { "w", "w", "e", "e", "e", "c" })[0];
        wordForCloud.WordSize.Location.Should()
            .Be(new Point(Center.X - wordForCloud.WordSize.Width / 2,
                Center.Y - wordForCloud.WordSize.Height / 2));
    }

    [Test]
    public void HaveDescendingOrder()
    {
        var wordsForCloud = tagGenerator.Generate(new List<string> { "e", "w", "w" });
        wordsForCloud[0].Font.Size.Should().BeGreaterThan(wordsForCloud[1].Font.Size);
    }

    [Test]
    public void PictureCreation_WithBoringWords()
    {
        var tagCloudCreatorWithBoringWords = tagCloudCreatorFactory
            .Get(new Size(2000, 2000),
                new Point(1000, 1000),
                new[] { Color.Black, Color.Blue, },
                "Arial",
                50,
                "in.txt",
                null);
        tagCloudCreatorWithBoringWords.GetCloud().Save("WithBoringWords.png", ImageFormat.Png);
        File.Exists("WithBoringWords.png").Should().BeTrue();
    }

    [Test]
    public void PictureCreation_WithoutBoringWords()
    {
        var tagCloudCreatorWithoutBoringWords = tagCloudCreatorFactory
            .Get(new Size(2000, 2000),
                new Point(1000, 1000),
                new[] { Color.Black, Color.Blue, },
                "Arial",
                50,
                "in.txt",
                "boringWords.txt");
        tagCloudCreatorWithoutBoringWords.GetCloud().Save("WithoutBoringWords.png", ImageFormat.Png);
        File.Exists("WithoutBoringWords.png").Should().BeTrue();
    }
}