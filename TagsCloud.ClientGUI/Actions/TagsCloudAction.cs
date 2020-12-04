using System.Drawing;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Core;

namespace TagsCloud.ClientGUI.Actions
{
    public class TagsCloudAction : IUiAction
    {
        private readonly SpiralSettings spiralSettings;
        private readonly TagsCloudPainter tagsCloudPainter;

        public TagsCloudAction(TagsCloudPainter tagsCloudPainter, SpiralSettings spiralSettings)
        {
            this.tagsCloudPainter = tagsCloudPainter;
            this.spiralSettings = spiralSettings;
        }

        public string Category => "Облако тегов";
        public string Name => "Нарисовать облако тегов";
        public string Description => "Рисование облака тегов по спирали";

        public void Perform()
        {
            var imageSize = tagsCloudPainter.PictureBox.GetImageSize();
            var spiral = new ArchimedeanSpiral(new Point(imageSize.Width / 2, imageSize.Height / 2),
                spiralSettings.SpiralParameter);
            var cloud = new CircularCloudLayouter(spiral);

            tagsCloudPainter.Paint(cloud);
        }
    }
}