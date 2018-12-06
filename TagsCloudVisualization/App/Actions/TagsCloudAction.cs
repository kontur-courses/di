using System.Windows.Forms;
using TagsCloudVisualization.TagsCloud;
using TagsCloudVisualization.TagsCloud.CircularCloud;

namespace TagsCloudVisualization.App.Actions
{
    public class TagsCloudAction : IUiAction
    {
        public string Name => "ОблакоТэгов";
        public string Category => "Изображение";
        private readonly TagsCloudVisualizer visualizer;
        private readonly PictureBoxImageHolder imageHolder;

        public TagsCloudAction(TagsCloudVisualizer visualizer, PictureBoxImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
            this.visualizer = visualizer;
        }
        public void Perform()
        {
            var image = visualizer.DrawCircularCloud();
            imageHolder.RecreateImage(image);
            imageHolder.Refresh();
            Application.DoEvents();
        }
    }
}