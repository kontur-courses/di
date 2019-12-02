using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Models
{
    public class Tag
    {
        public Size Size { get;}
        public string Text { get; }
        public Tag(string text,Size size)
        {
            Size = size;
            Text = text;
        }
    }
}
