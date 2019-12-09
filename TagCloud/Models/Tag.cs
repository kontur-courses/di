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
        public int Count { get;}
        public string Text { get; }
        public Font Font { get; }

        public Tag(string text,int count,Font font)
        {
            Count = count;
            Text = text;
            Font = font;
        }
    }
}
