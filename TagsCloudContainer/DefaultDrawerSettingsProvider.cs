using System.Drawing;

namespace TagsCloudContainer
{
    public class DefaultDrawerSettingsProvider : ISettingsProvider
    {
        public DefaultDrawerSettings DefaultDrawerSettings { get; }

        public DefaultDrawerSettingsProvider()
        {
            DefaultDrawerSettings = new DefaultDrawerSettings()
            {
                BackgroundColor = Color.Khaki,
                FontColor = Color.Black,
                Font = new Font(FontFamily.GenericMonospace, 15),
            };
        }
    }
}