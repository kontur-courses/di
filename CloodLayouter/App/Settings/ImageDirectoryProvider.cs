using System.IO;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class ImageDirectoryProvider : IImageDirectoryProvider
    {
        public string ImageDirectory => Directory.GetCurrentDirectory();
    }
}