using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TagsCloudContainer.Visualizer
{
    public class WordRectangle
    {
        public string Word { get; }
        public Rectangle Rectangle { get; }

        public WordRectangle(string word, Rectangle rectangle)
        {
            Word = word;
            Rectangle = rectangle;
        }
    }
}
