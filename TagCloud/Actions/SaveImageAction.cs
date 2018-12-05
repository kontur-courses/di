using System.IO;
using System.Windows.Forms;
using TagCloud.Forms;

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
                openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|PNG files (*.png)|*.png|TIF files (*.tif)|*.tif|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imageBox.SaveImage(openFileDialog.FileName);
                }
            }

            
        }
    }
}