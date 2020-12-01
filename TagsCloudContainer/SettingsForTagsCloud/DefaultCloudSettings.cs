using System.Drawing;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public static class DefaultCloudSettings
    {
        public static readonly Color BackgroundColor = Color.White;
        public static readonly Color TextColor = Color.LimeGreen;
        public static readonly Size ImageSize = Size.Empty;
        public static readonly Font Font = new Font("arial", 10);
        public static readonly string PathToSave = "cloud.png";
        public static readonly string[] CustomBoringWords = new string[0];
    }
}