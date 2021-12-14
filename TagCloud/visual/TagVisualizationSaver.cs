using System.Drawing;
using TagCloud.configurations;

namespace TagCloud.visual
{
    public class TagVisualizationSaver : ISaver<Image>
    {
        private readonly IImageSaveConfiguration saveConfiguration;

        public TagVisualizationSaver(IImageSaveConfiguration saveConfiguration)
        {
            this.saveConfiguration = saveConfiguration;
        }

        public void Save(Image image)
        {
            image.Save(saveConfiguration.GetFilename(), saveConfiguration.GetImageFormat());
        }
    }
}