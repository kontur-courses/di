using System;
using System.Windows.Forms;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.UI.Actions
{
    public class ChooseFilePathAction : IUiAction
    {

        private readonly IDocumentPathProvider pathProvider;
        public string Name { get; }

        public ChooseFilePathAction(IDocumentPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
            Name = "Choose text file path";
        }

        public void Perform(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog {Filter = "txt files (*.txt)|*.txt"};
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pathProvider.SetPath(fileDialog.FileName);
            }
        }
    }
}