using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagsCloudVisualization.Actions
{
    public class CreateCloudFromFileAction : IUiAction
    {
        private readonly IVisualizer visualizer;
        private readonly PictureBox imageHolder;

        public CreateCloudFromFileAction(IVisualizer visualizer, PictureBox imageHolder)
        {
            this.imageHolder = imageHolder;
            this.visualizer = visualizer;
            Name = "Create From File";
        }

        public string Name { get; }
        public void Perform()
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                var image = visualizer.VisualizeTextFromFile(fileToOpen, ImageSettings.DefaultSettings);
                imageHolder.Image = image;
            }
        }
    }
}