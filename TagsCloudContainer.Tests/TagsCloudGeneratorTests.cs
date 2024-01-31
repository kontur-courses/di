using System.Drawing;
using Autofac;
using FluentAssertions;
using TagsCloudContainer.CloudGenerators;
using TagsCloudContainer.Settings;
using Program = ConsoleApp.Program;

namespace TagsCloudContainer.Tests;

[TestFixture]
[TestOf(typeof(TagsCloudGenerator))]
public class TagsCloudGeneratorTests
{
    private TagsCloudGenerator sut;
    private ImageSettings imageSettings;
    
    [OneTimeSetUp]
    public void Setup()
    {
        var builder = new ContainerBuilder();
        Program.ConfigureService(builder);
        builder.RegisterType<TagsCloudGenerator>().AsSelf();
        var scope = builder.Build();
        sut = scope.Resolve<TagsCloudGenerator>();
        imageSettings = new ImageSettings();
    }

    [Test]
    public void Should_LocatePopularWordInCenter()
    {
        var popularWord = "bbb";
        var center = new Point(imageSettings.ImageSize.Width / 2, imageSettings.ImageSize.Height / 2);
        var wordsDetails = new[]
        {
            new WordDetails("aaa", 4),
            new WordDetails(popularWord, 10),
            new WordDetails("ccc", 4),
        };

        var cloud = sut.Generate(wordsDetails);
        
        var tag = cloud.Tags.First(tag => tag.Word == popularWord);
        tag.Rectangle.Contains(center).Should().BeTrue();
    }
    
    [Test]
    public void Should_ContainsAllWords()
    {
        var words = new[] { "a", "b", "c", "d", "e", "f", "g" };
        var wordsDetails = words
            .Select(word => new WordDetails(word))
            .ToArray();

        var cloud = sut.Generate(wordsDetails);
        
        cloud.Tags.Select(tag => tag.Word).Should().BeEquivalentTo(words);
    }
}