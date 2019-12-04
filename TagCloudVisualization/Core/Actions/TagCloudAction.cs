using TagCloudVisualization.Core.Painter;
using TagCloudVisualization.Infrastructure.Common;
using TagCloudVisualization.Infrastructure.UiActions;

namespace TagCloudVisualization.Core.Actions
{
    public class TagCloudAction : IUiAction
    {
        private readonly TagCloudPainter.Factory tagCloudPainterFactory;
            
        private readonly TagCloudSettings tagCloudSettings;

        public TagCloudAction(TagCloudSettings tagCloudSettings,
            TagCloudPainter.Factory tagCloudPainterFactory)
        {
            this.tagCloudSettings = tagCloudSettings;
            this.tagCloudPainterFactory = tagCloudPainterFactory;
        }

        public MenuCategory Category => MenuCategory.Cloud;
        public string Name => "Спиралевидное облако";
        public string Description => "Облако тегов с использованием спирали Архимеда";

        public void Perform()
        {
            SettingsForm.For(tagCloudSettings).ShowDialog();
            tagCloudPainterFactory(tagCloudSettings).Paint();
        }
    }
}