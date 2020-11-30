using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloud.BitmapCreator
{
    interface IBitmapCreator
    {
        public Bitmap Create(IEnumerable<string> words);
    }
}
