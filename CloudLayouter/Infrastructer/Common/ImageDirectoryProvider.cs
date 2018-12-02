namespace CloudLayouter.Infrastructer.Common
{
    public class ImageDirectoryProvider : IImageDirectoryProvider
    {
        public string ImagesDirectory => System.IO.Directory.GetCurrentDirectory();
    }
}