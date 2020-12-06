using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.Settings
{
    public class AppSettings
    { 
        public AppSettings()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            ImageSettings = new ImageSettings();
            FontSettings = new FontSettings();
            Palette = new Palette();
            Format = ImageFormat.Png;
            InputFileName = Path.Combine(Directory.GetCurrentDirectory(), 
                "..", "..", "..", "text.txt");
            OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), 
                "..", "..", "..");
            LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
        }

        public FontSettings FontSettings { get; set; }
        public ImageFormat Format { get; set; }
        public ImageSettings ImageSettings { get; set; }
        public string InputFileName { get; set; }
        public CloudLayouterAlgorithm LayouterAlgorithm { get; set; }
        public Palette Palette { get; set; }
        public string OutputDirectory { get; set; }
    }
}