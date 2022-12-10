using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloudContainer.Infrastructure
{
    public class ImageDirectoryProvider : IImageDirectoryProvider
    {
        public string ImagesDirectory => ".";
    }
}
