using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.MorphAnalyzer;
using TagsCloudVisualization.Parsing;
using TagsCloudVisualization.Reading;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class TagCloudVisualizationsTests
{
    private TagCloudVisualizations _tagCloudVisualizations;
    private VisualizationOptions _defaultOptions;


    [SetUp]
    public void Setup()
    {
        var textReaderMock = new Mock<ITextReader>();
        textReaderMock.Setup(a => a.ReadText())
            .Returns("test1\r\ntest2\r\ntest3\r\ntest4\r\ntest5\r\ntest6\r\ntest7\r\ntest1\r\ntest2");

        var morphAnalyzerMock = new Mock<IMorphAnalyzer>();
        morphAnalyzerMock.Setup(a => a.GetWordsMorphInfo(It.IsAny<IEnumerable<string>>()))
            .Returns(new Func<IEnumerable<string>, Dictionary<string, WordMorphInfo>>((callInfo) => new Dictionary<string, WordMorphInfo>()));

        var wordsLoader = new CustomWordsLoader(textReaderMock.Object, new SingleColumnTextParser(), new CustomWordSizeCalculator(), new CustomWordsFilter(morphAnalyzerMock.Object));
        var cloudLayouter = new CircularCloudLayouter(new ArithmeticSpiral());
        _tagCloudVisualizations = new TagCloudVisualizations(cloudLayouter, wordsLoader);
        _defaultOptions = new VisualizationOptions()
        {
            Palette = new Palette(Brushes.Black),
            BackgroundColor = Color.White,
            CanvasSize = new Size(1024, 1024),
            FontFamily = new FontFamily("Arial"),
            SpiralStep = 0.1f,
            MinFontSize = 10,
            MaxFontSize = 30,
            TakeMostPopularWords = 10
        };
    }

    [Test]
    public void DrawCloud_ZeroCanvasSize_ThrowException()
    {
        _defaultOptions.CanvasSize = new Size(0, 0);
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<ArgumentException>();
    }

    [Test]
    public void DrawCloud_EmptyOptions_ThrowException()
    {
        _defaultOptions = new VisualizationOptions();
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<ArgumentException>();
    }

    [Test]
    public void DrawCloud_NullFontFamily_ThrowException()
    {
        _defaultOptions.FontFamily = null!;
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<NullReferenceException>();
    }

    [Test]
    public void DrawCloud_NullPalette_ThrowException()
    {
        _defaultOptions.Palette = null!;
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<NullReferenceException>();
    }


    [Test]
    public void DrawCloud_NullPaletteDefaultBrush_ThrowException()
    {
        _defaultOptions.Palette = new Palette(Brushes.Black);
        _defaultOptions.Palette.DefaultBrush = null!;
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<NullReferenceException>();
    }

    [Test]
    public void DrawCloud_NullPaletteAvailableBrushes_ThrowException()
    {
        _defaultOptions.Palette = new Palette(Brushes.Black);
        _defaultOptions.Palette.AvailableBrushes = null!;
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<NullReferenceException>();
    }

    [Test]
    public void DrawCloud_ZeroSpiralStep_ThrowException()
    {
        _defaultOptions.SpiralStep = 0;
        new Action(() => { _tagCloudVisualizations.DrawCloud(_defaultOptions); }).Should().Throw<ArgumentException>();
    }
    

    // if (options.CanvasSize.Width < 1 || options.CanvasSize.Height < 1)
    // throw new ArgumentException("Canvas size must be greater than 1");
    //
    //     if (options.FontFamily == null)
    // throw new NullReferenceException("FontFamily null");
    //
    //     if (options.Palette == null)
    // throw new NullReferenceException("FontFamily null");
    //
    //     if (options.Palette.DefaultBrush == null)
    // throw new NullReferenceException("FontFamily null");
    //
    //     if (options.Palette.AvailableBrushes == null)
    // throw new NullReferenceException("FontFamily null");
    //
    //     if (options.SpiralStep <= 0)
    // throw new ArgumentException("SpiralStep must be greater than 0");
}