using System.IO;
using CloudLayouter.Infrastructer.Interfaces;

namespace CloudLayouter.Infrastructer.Common
{
    public class ImageDirectoryProvider : IImageDirectoryProvider
    {
        public string ImagesDirectory => Directory.GetCurrentDirectory();
    }
}