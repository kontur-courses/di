using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TagsCloudVisualization.Canvases;

namespace TagsCloudVisualization.FormAction
{
    public class SaveImageAction : IFormAction
    {
        public string Category => "File";
        public string Name => "Save as image";
        public string Description => "Save your tag cloud to a folder";

        private readonly ICanvas canvas;

        public SaveImageAction(ICanvas canvas)
        {
            this.canvas = canvas;
        }

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = @"C:\Users\Public\Pictures",
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Images |*.bmp|Images |*.jpg;*.jpeg|Images |*.png|Images |*.gif"
            };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
                canvas.SaveImage(dialog.FileName, GetSaveImageFormat(dialog));
        }

        private ImageFormat GetSaveImageFormat(SaveFileDialog dialog)
        {
            return Path.GetExtension(dialog.FileName) switch
            {
                ".BMP" => ImageFormat.Bmp,
                ".Jpg" => ImageFormat.Jpeg,
                ".Gif" => ImageFormat.Gif,
                _ => ImageFormat.Png
            };
        }
    }
}