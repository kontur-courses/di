using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudContainer
{
    class TagSizeNormalizer : ITagSizeNormalizer
    {
        private readonly Font font;

        public TagSizeNormalizer(Font font)
        {
            this.font = font;
        }

        public Size GetTagSize(string word)
        {
            return TextRenderer.MeasureText(word, font);
        }
    }
}
