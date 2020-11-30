using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloud.ImageConfig
{
    public class ImageConfig : IImageConfig
    {
     public Size Size { get; }

        public ImageConfig(Size size)
        {
       
            Size = size;
        }
    }
}
