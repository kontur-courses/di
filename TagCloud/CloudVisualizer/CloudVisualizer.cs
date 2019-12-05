using System.Drawing;
using TagCloud.WordsPreprocessing;
using System.Windows.Forms;

namespace TagCloud.CloudVisualizer
{
    public class CloudVisualizer
    {
        private CloudViewConfiguration.CloudViewConfiguration cloudViewConfiguration; 

        public CloudVisualizer(CloudViewConfiguration.CloudViewConfiguration configuration)
        {
            cloudViewConfiguration = configuration;
        }

        public Bitmap GetCloud(Word[] words)
        {
            var cloudLayouter = cloudViewConfiguration.CloudLayouter();
            var image = new Bitmap(cloudViewConfiguration.ImageSize.Width, cloudViewConfiguration.ImageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(cloudViewConfiguration.BackgroundColor);
            foreach (var word in words)
            {
                var font = new Font(cloudViewConfiguration.FontFamily,
                    (float)(word.Frequency * cloudViewConfiguration.ScaleCoefficient));
                var size = TextRenderer.MeasureText(word.Value, font);
                var rectangle = cloudLayouter.PutNextRectangle(size);
                graphics.DrawString(word.Value, font, cloudViewConfiguration.GetBrush(word), rectangle.Location);
            }

            graphics.Dispose();

            return image;
        }
    }
}
