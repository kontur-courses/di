using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public class SizeFileReader : IReader
    {
        public IEnumerable<Size> Read(string[] lines)
        {
            foreach (var line in lines)
            {
                var dimensions = line.Split(' ');
                if (dimensions.Length != 2)
                    throw new ArgumentException();
                var widthIsCorrect = int.TryParse(dimensions[0], out var width);
                var heightIsCorrect = int.TryParse(dimensions[1], out var height);
                if (!widthIsCorrect || !heightIsCorrect)
                    throw new ArgumentException();
                yield return new Size(width, height);
            }
        }
    }
}