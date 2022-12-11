using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;
using TagCloudGui.Infrastructure;
using TagCloudGui.Infrastructure.Common;

namespace TagCloudGui.Actions
{
    public class WordTagCloudCreateAction : IUiAction
    {
        private readonly ITagCloudVisualizationSettings settings;
        private readonly ITagCloudVisualization visualization;
        private readonly ITagCloudCreator tagCloudCreator;
        private readonly IImageHolder imageHolder;

        public WordTagCloudCreateAction(
            IImageHolder imageHolder,
            ITagCloudVisualization visualization, 
            ITagCloudCreator tagCloudCreator,
            ITagCloudVisualizationSettings settings)
        {
            this.imageHolder = imageHolder;
            this.visualization = visualization;
            this.tagCloudCreator = tagCloudCreator;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Picture;
        public string Name => "Создание облака тегов";
        public string Description => "Создание облака тегов";


        public void Perform()
        {
            using (var graphics = imageHolder.StartDrawing())
            {
                var tagCloud = tagCloudCreator.GenerateTagCloud();
                visualization.PrepareImage(graphics, tagCloud, settings);
            }
            imageHolder.UpdateUi();
        }
    }
}
