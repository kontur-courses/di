using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudLibrary.Colorers
{
    public class BlackColorer : IColorer
    {
        public string Name { get; } = "frequency";
        public Color ColorForWord(string word, double factor)
        {
            return Color.Black;
        }
    }
}
