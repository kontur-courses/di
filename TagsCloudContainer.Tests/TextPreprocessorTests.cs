using Autofac;
using ConsoleApp;
using FluentAssertions;
using MyStemWrapper;
using TagsCloudContainer.TextAnalysers;

namespace TagsCloudContainer.Tests;

[TestFixture]
[TestOf(typeof(TextPreprocessor))]
public class TextPreprocessorTests
{
    private TextPreprocessor sut;

    [OneTimeSetUp]
    public void Setup()
    {
        var builder = new ContainerBuilder();
        Program.ConfigureService(builder);
        builder.RegisterType<TextPreprocessor>().AsSelf();
        
        var scope = builder.Build();
        sut = scope.Resolve<TextPreprocessor>();
        var myStem = scope.Resolve<MyStem>();
        // myStem.PathToMyStem = "";
    }
    
    [Test]
    public void Should_NoContainsEmptyWords()
    {
        var text = "собака  . !   ";
        
        var analyzeData = sut.Preprocess(text);

        analyzeData.WordData.Should().HaveCount(1);
        analyzeData.WordData.Should().BeEquivalentTo(new[] {new WordData("собака", 1)});
    }
    
    [Test]
    public void Should_TransformToLowerCase()
    {
        var text = "Собака Любить Кость";
        var expected = text.Split().Select(word => word.ToLower());
        
        var analyzeData = sut.Preprocess(text);

        analyzeData.WordData.Select(word => word.Word).Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void Should_TransformToInitialForm()
    {
        var text = "собаки любят красивых собак";
        var expected = new[] { "собака", "любить", "красивый" };

        var analyzeData = sut.Preprocess(text);

        analyzeData.WordData.Select(word => word.Word).Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void Should_IgnoreBoringWords()
    {
        var text = "я не ваш только с от к мы нашему кому";
        
        var analyzeData = sut.Preprocess(text);

        analyzeData.WordData.Should().BeEmpty();
    }
}