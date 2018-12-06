using CloudLayouter.Infrastructer.Common;
using CloudLayouter.Infrastructer.Common.Settings;
using CloudLayouter.Infrastructer.Interfaces;
using CloudLayouter.Painters;

namespace CloudLayouter.Actions
{
    public class TagLayoutAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        private readonly TagCloudPainter painter;

        public TagLayoutAction(TagCloudPainter painter, IImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageSettings = imageSettings;
            this.imageHolder = imageHolder;
            this.painter = painter;
        }

        public string Name => "Draw tag cloud";
        public string Description => "...";

        public void Perform()
        {
            imageHolder.RecreateImage(imageSettings);
            painter.Paint();
        }

        public MenuCategory Category => MenuCategory.DrawTagCloud;
    }
}