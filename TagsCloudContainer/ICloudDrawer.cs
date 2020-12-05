using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface ICloudDrawer
    {
        public int ImageSize { get; set; }
        public IColorProvider ColorProvider { get; set; }
        public IImageSaver ImageSaver { get; set; }
        public void DrawCloud(List<WordWithFont> words, string targetPath);
        public void ChangeImageSize(int newSize);
    }
}