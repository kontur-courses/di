using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TagCloud2;

namespace TagCloud2Tests
{
    public class TagCloud2FullTests
    {
        private readonly Generator generator = new();

        private readonly List<string> wordList = new();

        private readonly List<Rectangle> rectangleList = new();

        private readonly TestOptions options = new();

        [SetUp]
        public void SetUp()
        {
            wordList.Clear();
            rectangleList.Clear();
            options.SetToDefault();
            options.Path = "input.txt";
        }

        [Test]
        public void TestOnDefaultSettingsWithOneWord()
        {
            rectangleList.Add(new Rectangle(479, 490, 42, 20));
            wordList.Add("dddd");
            CreateInputFileWithWords();
            generator.Generate(options);

            Assert();
        }

        [Test]
        public void TestOnNonDefaultSettingsWithTwoWords()
        {
            options.OutputName = "aboba.bmp";
            options.Path = "not input at all.txt";
            options.AngleSpeed = 1;
            options.FontName = "Comic Sans MS";
            options.FontSize = 20;
            options.LinearSpeed = 10;
            options.X = 2000;
            options.Y = 500;
            wordList.Add("пропал");
            wordList.Add("котёнок");
            rectangleList.Add(new Rectangle(946, 230, 108, 41));
            rectangleList.Add(new Rectangle(958, 183, 112, 41));

            CreateInputFileWithWords();
            generator.Generate(options);

            Assert();
        }

        private void Assert()
        {
            var result = (Bitmap)Image.FromFile(options.OutputName);
            var expected = new Bitmap(options.X, options.Y);
            DrawWhiteStrings(expected, wordList, rectangleList, options);

            BitmapsRChannelsAreEqual(expected, result).Should().BeTrue();
        }

        private bool BitmapsRChannelsAreEqual(Bitmap expected, Bitmap result)
        {
            for (int x = 0; x < options.X; x += 2)
            {
                for (int y = 0; y < options.Y; y += 2)
                {
                    var expectedPixel = expected.GetPixel(x, y).R;
                    var actualPixel = result.GetPixel(x, y).R;
                    if (expectedPixel != actualPixel)
                    {
                        throw new Exception("X:" + x + " Y:" + y + " differs.");
                    }
                }
            }

            return true;
        }

        private void DrawWhiteStrings(Bitmap bitmap, List<string> strings, List<Rectangle> rectangles, TestOptions options)
        {
            var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            for (int i = 0; i < strings.Count; i++)
            {
                g.DrawString(strings[i], 
                    new Font(options.FontName,
                    options.FontSize,
                    FontStyle.Regular), 
                    Brushes.White, 
                    rectangles[i]) ;
            }
        }

        private void CreateInputFileWithWords()
        {
            var SB = new StringBuilder();
            foreach (var word in wordList)
            {
                SB.Append(word);
                SB.Append(Environment.NewLine);
            }
            var text = SB.ToString();
            File.WriteAllText(options.Path, text);
        }
    }
}
