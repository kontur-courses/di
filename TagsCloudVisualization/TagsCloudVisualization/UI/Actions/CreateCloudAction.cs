using System;
using System.Windows.Forms;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.UI.Actions
{
    public class CreateCloudAction : IUiAction
    {
        private readonly IVisualizer visualizer;
        private readonly IImageSettingsProvider imageSettingsProvider;
        private readonly IImageHolder imageHolder;
        private readonly IDocumentPathProvider pathProvider;
        public string Name { get; }

        public CreateCloudAction(
            IVisualizer visualizer,
            IImageSettingsProvider imageSettingsProvider,
            IDocumentPathProvider pathProvider,
            IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
            this.visualizer = visualizer;
            this.imageSettingsProvider = imageSettingsProvider;
            this.pathProvider = pathProvider;
            Name = "Create";
        }

        public void Perform(object sender, EventArgs e)
        {
            if (pathProvider.TryGetPath(out var path))
            {
                var image = visualizer.VisualizeTextFromFile(path);
                imageHolder.Image = image;
            }
            else
            {
                MessageBox.Show("Invalid document path", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}