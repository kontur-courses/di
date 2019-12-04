using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace TagCloud.Models
{
    public class Tag
    {
        public Size Size { get;}
        public string Text { get; }
        public FontSize FSize { get; }
        public Tag(string text,Size size,FontSize fontSize)
        {
            FSize = fontSize;
            Size = size;
            Text = text;
        }
    }
}
