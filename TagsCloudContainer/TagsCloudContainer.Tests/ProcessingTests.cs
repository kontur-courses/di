using System.Collections.Generic;
using FluentAssertions;
using TagsCloudContainer.Processing;
using Xunit;

namespace TagsCloudContainer.Tests;

public class ProcessingTests
{
    [Fact]
    public void GoodWord()
    {
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var success = processor.TryProcess("foo", out var processed);
        success.Should().BeTrue();
        processed.Should().Be("foo");
    }

    [Fact]
    public void BoringWord()
    {
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var success = processor.TryProcess("are", out _);
        success.Should().BeFalse();
    }

    [Fact]
    public void CustomBoringWord()
    {
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions
            {BoringWords = new HashSet<string> {"foo"}});
        var success = processor.TryProcess("foo", out _);
        success.Should().BeFalse();
    }
    
    [Fact]
    public void Trim()
    {
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var success = processor.TryProcess("foo ", out var processedWord);
        success.Should().BeTrue();
        processedWord.Should().Be("foo");
    }
    
    [Fact]
    public void LowerCase()
    {
        var processor = new SimpleWordsProcessor(new SimpleWordsProcessorOptions());
        var success = processor.TryProcess("FoO", out var processedWord);
        success.Should().BeTrue();
        processedWord.Should().Be("foo");
    }
}