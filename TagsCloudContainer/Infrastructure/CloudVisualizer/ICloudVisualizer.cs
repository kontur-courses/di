using TagsCloudContainer.App.Settings;

namespace TagsCloudContainer.Infrastructure.CloudVisualizer
{
    internal interface ICloudVisualizer
    {
        public void Visualize(AppSettings appSettings);
    }
}