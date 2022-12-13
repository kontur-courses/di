using System.Drawing;

namespace TagsCloudContainer
{
    public class SettingsProvider : ISettingsProvider
    {
        public Settings Settings { get; }

        public SettingsProvider()
        {
            Settings = new Settings()
            {
                BackgroundColor = Color.Khaki,
                FontColor = Color.Black,
                Font = new Font(FontFamily.GenericMonospace, 15),
                StepSize = 10
            };
        }
    }
}