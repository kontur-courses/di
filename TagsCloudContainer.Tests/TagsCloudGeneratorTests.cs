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
        var analyzeData = new AnalyzeData
        {
            WordData = new[]
            {
                new WordData("aaa", 4),
                new WordData(popularWord, 10),
                new WordData("ccc", 4),
            }
        };

        var cloud = sut.Generate(analyzeData);
        
        var tag = cloud.Tags.First(tag => tag.Word == popularWord);
        tag.Rectangle.Contains(center).Should().BeTrue();
    }
    
    [Test]
    public void Should_ContainsAllWords()
    {
        var words = new[] { "a", "b", "c", "d", "e", "f", "g" };
        var analyzeData = new AnalyzeData
        {
            WordData = words.Select(word => new WordData(word, 1)).ToArray()
        };

        var cloud = sut.Generate(analyzeData);
        
        cloud.Tags.Select(tag => tag.Word).Should().BeEquivalentTo(words);
    }
}