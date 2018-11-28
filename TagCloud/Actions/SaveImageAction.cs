using System.Windows.Forms;

namespace TagCloud
{
    public class SaveImageAction : IUiAction
    {
        private readonly ImageBox imageBox;

        public SaveImageAction(ImageBox imageBox)
        {
            this.imageBox = imageBox;
        }

        public string Category => "Image";
        public string Name => "Save";
        public string Description => "Save image";

        public void Perform()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.CheckFileExists = false;
                openFileDialog.DefaultExt = "png";
                openFileDialog.FileName = "TagCloud.png";
                openFileDialog.Filter = "PNG (*.png)|*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    imageBox.SaveImage(openFileDialog.FileName);
            }

            
        }
    }
}