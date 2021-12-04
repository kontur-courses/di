﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Layouters
{
    public class Tag
    {
        public string Value { get; }
        public int Frequency { get; }
        public Size Size { get; }
        public Rectangle ContainingRectangle { get; }

        public Tag(string value, int frequency, Size size)
        {
            Value = value;
            Frequency = frequency;
            Size = size;
        }

        public Tag(Tag tag, Rectangle containingRectangle)
        {
            Value = new string(tag.Value);
            Frequency = tag.Frequency;
            Size = tag.Size;
            ContainingRectangle = containingRectangle;
        }
    }
}
