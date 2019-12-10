using TagCloud.Infrastructure;

namespace TagCloud.App
{
    public class AppSettings
    {
        public PictureConfig PictureConfig { get; set; }
        public string InputFilePath { get; set; }
        public string OutputFilePath{ get; set; }
    }
}
