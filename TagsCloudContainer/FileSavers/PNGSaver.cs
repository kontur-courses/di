using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudContainer.FileSavers
{
    public class PngSaver : IFileSaver
    {
        public string Path { get; }
        public Bitmap Canvas { get; set; }

        public PngSaver(CustomSettings settings)
        {
            Path = settings.PathToSave;
            Canvas = new Bitmap(settings.CanvasWidth, settings.CanvasHeight);
        }

        public void SaveCanvas() => Canvas.Save(Path, new ImageFormat(ImageFormat.Png.Guid));
    }
}