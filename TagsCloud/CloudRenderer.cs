using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace TagsCloud
{
    public class CloudRenderer
    {
        private const string SamplesDirectory = "Samples";
        private readonly string path;
        private readonly Brush brush = new HatchBrush(HatchStyle.Cross, Color.Black);

        public CloudRenderer()
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent;
            if (directoryInfo == null) return;
            path = $"{directoryInfo.FullName}\\{SamplesDirectory}";
            if(!Directory.Exists(path))
                directoryInfo.CreateSubdirectory(SamplesDirectory);
        }
        
        public string RenderCloud(WordLayouter layouter, int width, int height)
        {
            if (string.IsNullOrEmpty(path) || width <= 0 || height <= 0) return null;

            var wordsRectangle = layouter.CurrentCloudRectangle();
            var words = layouter.CloudWords;
            
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.ScaleTransform((float) width / wordsRectangle.Width, (float) height / wordsRectangle.Height);
            graphics.TranslateTransform(-wordsRectangle.X, -wordsRectangle.Y);
            foreach (var word in words)
            {
                graphics.DrawString(word.Value, word.Font, brush, word.Rectangle.Location);
            }
            
            graphics.Save();
            var imagePath = $"{path}\\sample.png";
            bitmap.Save(imagePath);
            return imagePath;
        }
    }
}