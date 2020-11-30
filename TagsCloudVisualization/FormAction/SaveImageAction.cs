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
                Filter = "Изображения (*.png)|*.bmp;*.png;*.jpg" 
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                canvas.SaveImage(dialog.FileName);
        }
    }
}