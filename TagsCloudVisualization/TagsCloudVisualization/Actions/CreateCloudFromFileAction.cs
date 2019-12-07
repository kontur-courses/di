using System.Windows.Forms;

namespace TagsCloudVisualization.Actions
{
    public class CreateCloudFromFileAction : IUiAction
    {
        private readonly IVisualizer visualizer;
        private readonly PictureBox imageHolder;
        private readonly ImageSettingsProvider imageSettingsProvider;
        public string Name { get; }

        public CreateCloudFromFileAction(
            IVisualizer visualizer, PictureBox imageHolder, ImageSettingsProvider imageSettingsProvider)
        {
            this.imageHolder = imageHolder;
            this.visualizer = visualizer;
            this.imageSettingsProvider = imageSettingsProvider;
            Name = "Create From File";
        }

        public void Perform()
        {
            var fileDialog = new OpenFileDialog {Filter = "txt files (*.txt)|*.txt"};
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileToOpen = fileDialog.FileName;
                var image = visualizer.VisualizeTextFromFile(fileToOpen, imageSettingsProvider.ImageSettings);
                imageHolder.Image = image;
            }
        }
    }
}