using System.Drawing;
using System.Collections.Generic;
using TagsCloud.WordProcessing;

namespace TagsCloud.Interfaces
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string path);
    }
}
