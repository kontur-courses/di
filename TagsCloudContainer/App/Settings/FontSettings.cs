using System.Drawing;

namespace TagsCloudContainer.App.Settings
{
    public class FontSettings
    {
        public FontSettings()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            Font = new Font("Arial", 10);
        }

        public Font Font { get; set; }
    }
}