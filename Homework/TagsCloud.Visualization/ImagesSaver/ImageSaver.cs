using System;
using System.Drawing;
using System.IO;

namespace TagsCloud.Visualization.ImagesSaver
{
    public class ImageSaver : IImageSaver
    {
        private readonly SaveSettings settings;

        public ImageSaver(SaveSettings settings)
        {
            this.settings = settings;
        }

        public void Save(Image image)
        {
            if (!Directory.Exists(settings.OutputDirectory))
                throw new Exception($"directory {settings.OutputDirectory} not found");
            
            var path = Path.Combine(settings.OutputDirectory, $"{settings.OutputFileName}.{settings.Extension}");
            image.Save(path);
        } 
    }
}