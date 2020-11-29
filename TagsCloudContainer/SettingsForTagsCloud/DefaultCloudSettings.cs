using System.Drawing;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public static class DefaultCloudSettings
    {
        public static readonly Color BackgroundColor = Color.White;
        public static readonly Color TextColor = Color.LimeGreen;
        public static readonly Size ImageSize = new Size(800, 700);
        public static readonly Font Font = new Font("arial", 10);
        public static readonly string PathToSave = "cloud.png";
    }
}