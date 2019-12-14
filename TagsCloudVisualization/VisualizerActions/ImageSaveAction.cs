using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.VisualizerActions
{
    public abstract class ImageSaveAction : IVisualizerAction
    {
        protected readonly AppSettings appSettings;

        private readonly Dictionary<string, ImageFormat> formats = new Dictionary<string, ImageFormat>()
        {
            ["png"] = ImageFormat.Png, 
            ["bmp"] = ImageFormat.Bmp,
            ["jpg"] = ImageFormat.Jpeg
        };

        public ImageSaveAction(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public abstract string GetActionDescription();

        public abstract string GetActionName();

        protected abstract bool TryGetCorrectFileToSave(out string filepath);

        public void Perform()
        {
            if (TryGetCorrectFileToSave(out var filepath))
            {
                var format = filepath.Split('.').Last();
                var imageFormat = formats[format];
                appSettings.ImageHolder.SaveImage(filepath, imageFormat);
            }
        }
    }
}