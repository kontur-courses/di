using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;
using TagCloud.Creators;
using TagCloud.Layouters;
using TagCloud.Readers;
using TagCloud.UI.Console;
using TagCloud.Visualizers;
using TagCloud.Writers;

namespace TagCloudTests
{
    public class IntegrationTest
    {
        [Test]
        public void ClientRun_ShouldCreateFileWithTagCloud()
        {
            var args = new[] {"-i", "test.txt", "-o", "test.png"};
            var fileReader = new FileReaderFactory();
            var boringWordsFilter = new BoringWordsFilter();
            var textAnalyzer = new TextAnalyzer(
                new [] {boringWordsFilter}, 
                new [] {new WordsToLowerConverter()},
                new FrequencyAnalyzer());
            var tagCreator = new TagCreatorFactory();
            var tagColoringFactory = new TagColoringFactory();
            var layouterFactory = new CircularCloudLayouterFactory();
            var visualizer = new CloudVisualizer();
            var fileWriter = new BitmapWriter();
            var fileInfo = new FileInfo(Environment.CurrentDirectory + "\\test.txt");
            using (var writer = fileInfo.CreateText())
                writer.Write("test\ntext\ntest\ntext\na\nb");
            var fileWithBoringWords = new FileInfo(Environment.CurrentDirectory + "\\excluded.txt");
            using (var writer = fileWithBoringWords.CreateText())
                writer.Write("a\nb");


            var client = new ConsoleUI(fileReader, 
                textAnalyzer,
                boringWordsFilter,
                layouterFactory, 
                visualizer, 
                fileWriter, 
                tagCreator,
                tagColoringFactory);
            client.Run(args);

            var image = new FileInfo(Environment.CurrentDirectory + "\\test.txt");
            image.Exists.Should().BeTrue();
            image.Delete();
            fileInfo.Delete();
            fileWithBoringWords.Delete();
        }
    }
}
