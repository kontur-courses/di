using System.Drawing;

namespace TagsCloudContainer.App.Settings
{
    public class FontSettings
    {
        public static readonly FontSettings Default = new FontSettings(new Font("Arial", 10));

        private FontSettings(Font font)
        {
            Font = font;
        }

        public Font Font { get; set; }
    }
}