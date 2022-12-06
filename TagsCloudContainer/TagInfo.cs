using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudContainer
{
    public class TagInfo
    {
        public string TagText { get; set; }
        public Font TagFont { get; set; }
        public Rectangle TagRect { get; set; }

        public TagInfo(string tagText, Font tagFont)
        {
            TagText = tagText;
            TagFont = tagFont;
        }
    }
}