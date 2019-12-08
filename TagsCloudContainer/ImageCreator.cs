using System.Drawing;

namespace TagsCloudContainer
{
    public class ImageCreator
    {
        private Compositor compositor;
        private ImageSetting setting;
        
        public ImageCreator(ImageSetting setting, Compositor compositor)
        {
            this.setting = setting;
            this.compositor = compositor;
        }

        public void Save()
        {
            var words = compositor.Composite();
            
            using (var bitmap = new Bitmap(setting.Width, setting.Height))
            {
                var graphic = Graphics.FromImage(bitmap);
                foreach (var (rectangle, layoutWord) in words)
                {
                    graphic.DrawString(layoutWord.Word, layoutWord.Font, layoutWord.Brush, rectangle);                 
                }

                bitmap.Save("WordsCloud.png",System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}