using System.Drawing;

namespace TagCloud
{
    public class TagCloudSettings
    {
        public int MaxTextSize { get; set; }
        public int MinTextSize { get; set; }


        public static TagCloudSettings Default()
        {
            var settings = new TagCloudSettings();
            settings.MaxTextSize = 16;
            settings.MinTextSize = 8;
            return settings;
        }
    }
}
