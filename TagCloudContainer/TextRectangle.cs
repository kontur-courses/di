﻿using System.Drawing;

namespace TagsCloudVisualization
{
    public class TextRectangle
    {
        public readonly Rectangle rectangle;
        public readonly string text;
        public readonly Font font;
        public TextRectangle(Rectangle rectangle, string text, Font font)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));
            this.rectangle = rectangle;
            this.text = text;
            this.font = font ?? throw new ArgumentNullException(nameof(font));
        }
    }
}