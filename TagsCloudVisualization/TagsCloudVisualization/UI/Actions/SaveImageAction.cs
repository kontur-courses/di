using System;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.UI.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        public string Name { get; }

        public SaveImageAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
            Name = "Save";
        }

        public void Perform(object sender, EventArgs e)
        {
            var saveDialog = new SaveFileDialog {Filter = "Images|*.png"};
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (imageHolder.Image != null)
                    imageHolder.Image.Save(saveDialog.FileName, ImageFormat.Png);
                else
                    MessageBox.Show("No tag cloud to save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}