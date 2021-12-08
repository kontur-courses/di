using System.Drawing;
using TagCloud.configurations;

namespace TagCloud.visual
{
    public class TagVisualizationSaver : ISaver
    {
        private readonly IVisualizer visualizer;
        private readonly IImageSaveConfiguration saveConfiguration;
        private readonly IImageConfiguration imageConfiguration;

        public TagVisualizationSaver(
            IVisualizer visualizer,
            IImageSaveConfiguration saveConfiguration,
            IImageConfiguration imageConfiguration
        )
        {
            this.visualizer = visualizer;
            this.saveConfiguration = saveConfiguration;
            this.imageConfiguration = imageConfiguration;
        }

        public void Save()
        {
            using var bitmap = new Bitmap(imageConfiguration.GetWidth(), imageConfiguration.GetHeight());
            visualizer.FillImage(bitmap, imageConfiguration);
            bitmap.Save(saveConfiguration.GetFilename(), saveConfiguration.GetImageFormat());
        }
    }
}