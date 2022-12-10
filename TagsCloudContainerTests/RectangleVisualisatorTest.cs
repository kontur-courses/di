using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer;
using TextReader = TagsCloudContainer.TextReader;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class RectangleVisualisatorTest
    {
        private CircularCloudLayouter _layouter;
        private Dictionary<string, int> _words;
        private string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        [SetUp]
        public void SetUp()
        {
            _layouter = new CircularCloudLayouter(new Point(0, 0));
            _words = GetWords();
        }


        private Dictionary<string, int> GetWords()
        {
            var text = TextReader.GetTextFromFile($"{_projectDirectory}\\Example.txt");
            var handler = new WordHandler("BoringWords.txt");
            var words = handler.ProcessWords(text);
            return words;
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var visualisator = new RectangleVisualisator(_words, _layouter);

                visualisator.Paint();

                visualisator.Save(_projectDirectory, TestContext.CurrentContext.Test.Name, ImageFormat.Png);
            }
        }
    }
}