using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloud2;
using TagCloud2.Image;

namespace TagCloud2Tests
{
    public class TagCloud2FullTests
    {
        [Test]
        public void Test1()
        {
            var inputPath = "input.txt";
            var generator = new Generator();
            var options = new TestOptions() { Path = inputPath };
            var wordList = new List<string>();
            var rectangleList = new List<Rectangle>();
            rectangleList.Add(new Rectangle(479, 490, 42, 20));
            wordList.Add("dddd");
            CreateInputFileWithWords(inputPath, wordList);
            options.SetToDefault();
            generator.Generate(options);
            var result = (Bitmap)Bitmap.FromFile("output.bmp");
            var expected = new Bitmap(1000, 1000);
            DrawStrings(expected, wordList, rectangleList);

            BitmapsAreEqual(expected, result).Should().BeTrue();
        }

        private bool BitmapsAreEqual(Bitmap expected, Bitmap result)
        {
            for (int x = 0; x < 1000; x = x + 2)
            {
                for (int y = 0; y < 1000; y = y + 2)
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

        private void DrawStrings(Bitmap bitmap, List<string> strings, List<Rectangle> rectangles)
        {
            var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            for (int i = 0; i < strings.Count; i++)
            {
                g.DrawString(strings[i], new Font("Arial", 12, FontStyle.Regular), Brushes.White, rectangles[i]) ;
            }
        }

        private void CreateInputFileWithWords(string path, List<string> words)
        {
            var SB = new StringBuilder();
            foreach (var word in words)
            {
                SB.Append(word);
                SB.Append(Environment.NewLine);
            }
            var text = SB.ToString();
            File.WriteAllText(path, text);
        }
    }
}
