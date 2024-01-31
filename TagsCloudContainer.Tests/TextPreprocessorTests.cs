using Autofac;
using ConsoleApp;
using FluentAssertions;
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
    }
    
    [Test]
    public void Should_NoContainsEmptyWords()
    {
        var text = "собака  . !   ";
        
        var wordsDetails = sut.Preprocess(text);

        wordsDetails.Should().HaveCount(1);
        wordsDetails.Should().BeEquivalentTo(new[] {new WordDetails("собака", speechPart: "S")});
    }
    
    [Test]
    public void Should_TransformToLowerCase()
    {
        var text = "Собака Любить Кость";
        var expected = text.Split().Select(word => word.ToLower());
        
        var wordsDetails = sut.Preprocess(text);

        wordsDetails.Select(word => word.Word).Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void Should_TransformToInitialForm()
    {
        var text = "собаки любят красивых собак";
        var expected = new[] { "собака", "любить", "красивый" };

        var wordsDetails = sut.Preprocess(text);

        wordsDetails.Select(word => word.Word).Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void Should_IgnoreBoringWords()
    {
        var text = "я не ваш только с от к мы нашему кому";
        
        var wordsDetails = sut.Preprocess(text);

        wordsDetails.Should().BeEmpty();
    }
}