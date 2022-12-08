using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Linq;
using TagsCloudContainer;
using TextReader = TagsCloudContainer.TextReader;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CircularCloudLayouterTest
    {
        private Point _center;
        private CircularCloudLayouter _layouter;
        private Random _random;
        private List<Rectangle> _rectangles;
        private Dictionary<string, int> _words;
        private string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        
        [SetUp]
        public void SetUp()
        {
            _center = new Point(200, 200);
            _layouter = new CircularCloudLayouter(_center);
            _random = new Random(1);
            _rectangles = new List<Rectangle>();
            _words = GetWords();
        }

        public Dictionary<string, int> GetWords()
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
        
        [TestCase(0, 0, Description = "Empty size")]
        [TestCase(-1, -1, Description = "Less than 0 size")]
        public void PutNextRectangle_ThrowArgumentException(int sizeX, int sizeY)
        {
             Action action = () => _layouter.PutNextRectangle(new Size(sizeX, sizeY));
             action.Should().Throw<ArgumentException>().WithMessage("The size must not be equal to or less than 0");
        }
        
 
        [TestCase(100)]
        public void PutNextRectangle_ShouldNotIntersects(int amount)
        {
            var isIntersects = false;
            for (int i = 0; i < amount; i++)
            {
                var size = new Size(_random.Next() % 255 + 1, _random.Next() % 255 + 1);
                var rectangle = _layouter.PutNextRectangle(size);
                if (rectangle.IsIntersects(_rectangles))
                {
                    isIntersects = true;
                    isIntersects.Should().BeFalse();
                }
                _rectangles.Add(rectangle);
            }
            isIntersects.Should().BeFalse();
        }


        [TestCase(100)]
        public void PlacedRectangles_ShouldFillEllipseBy70Percent(int amount)
        {
            GenerateRectangles(amount);

            var yDiameter = _rectangles.Max(x => x.Bottom) - _rectangles.Min(x => x.Top);
            var xDiameter = _rectangles.Max(x => x.Right) - _rectangles.Min(x => x.Left);
            
            var filledArea = _rectangles.Sum(x => x.GetArea());

            var circleArea = Math.PI * Math.Pow(Math.Max(yDiameter, xDiameter), 2) / 4;

            var filledPercent = Convert.ToInt32((filledArea / circleArea) * 100);
            
            filledPercent.Should().BeGreaterThanOrEqualTo(70);
        }
        
        private void GenerateRectangles(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException();

            for (int i = 0; i < amount; i++)
            {
                var size = new Size(_random.Next() % 255 + 1, _random.Next() % 255 + 1);
                _rectangles.Add(_layouter.PutNextRectangle(size));
            }
        }
    }
}