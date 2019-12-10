using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudLibrary.Colorers
{
    public class ConstantColorer : IColorer
    {
        public string Name { get; } = "Constant";
        private Color color;
        public ConstantColorer(Color color)
        {
            this.color = color;
        }

        public Color ColorForWord(string word, double factor)
        {
            return color;
        }
    }
}
