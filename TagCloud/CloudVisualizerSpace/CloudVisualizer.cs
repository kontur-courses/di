using System.Drawing;
using System.Windows.Forms;
using TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace;
using TagCloud.WordsPreprocessing;

namespace TagCloud.CloudVisualizerSpace
{
    public class CloudVisualizer
    {
        private CloudViewConfiguration cloudViewConfiguration; 

        public CloudVisualizer(CloudViewConfiguration configuration)
        {
            cloudViewConfiguration = configuration;
        }

        public Bitmap GetCloud(Word[] words)
        {
            var cloudLayouter = cloudViewConfiguration.CloudLayouter();
            var image = new Bitmap(cloudViewConfiguration.ImageSize.Width, cloudViewConfiguration.ImageSize.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.Clear(cloudViewConfiguration.BackgroundColor);
                foreach (var word in words)
                {
                    var font = new Font(cloudViewConfiguration.FontFamily,
                        (float)(word.Frequency * cloudViewConfiguration.ScaleCoefficient + 1));
                    var size = TextRenderer.MeasureText(word.Value, font) + new Size(1, 1);
                    var rectangle = cloudLayouter.PutNextRectangle(size);
                    graphics.DrawString(word.Value, font, cloudViewConfiguration.GetBrush(), rectangle.Location);
                }
            }

            return image;
        }
    }
}
