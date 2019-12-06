using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TagsCloudVisualization.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly PictureBox imageHolder;
        public string Name { get; }

        public SaveImageAction(PictureBox imageHolder)
        {
            this.imageHolder = imageHolder;
            Name = "Save";
        }
        public void Perform()
        {
            var saveDialog = new SaveFileDialog {Filter = "Images|*.png"};
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (imageHolder.Image != null)
                {
                    imageHolder.Image.Save(saveDialog.FileName, ImageFormat.Png);
                }
                else
                {
                    MessageBox.Show(
                        "No tag cloud to save",  
                        "Warning", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}