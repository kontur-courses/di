using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class TagToken : WordToken
    {
        public Rectangle Rectangle { get; }

        public TagToken(WordToken wordToken, Rectangle rectangle) : base(wordToken.Word, wordToken.Count)
        {
            Rectangle = rectangle;
        }
    }
}
