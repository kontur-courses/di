using TagCloud.TagCloudVisualizations;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui.Actions
{
    public class WordTagCloudCreateAction : IUiAction
    {
        private readonly ITagCloudVisualizationSettings settings;
        private readonly ITagCloudVisualization visualization;
        private readonly IImageHolder imageHolder;

        public WordTagCloudCreateAction(
            IImageHolder imageHolder,
            ITagCloudVisualization visualization, 
            ITagCloudVisualizationSettings settings)
        {
            this.imageHolder = imageHolder;
            this.visualization = visualization;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Picture;
        public string Name => "Создание облака тегов";
        public string Description => "Создание облака тегов";


        public void Perform()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                visualization.PrepareImage(graphics, settings);
            }
            imageHolder.UpdateUi();
        }
    }
}
