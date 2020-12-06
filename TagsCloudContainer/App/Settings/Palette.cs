using System.Drawing;

namespace TagsCloudContainer.App.Settings
{
    public class Palette
    {

        public Palette()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            TextColor = Color.White;
            BackgroundColor = Color.Black;
        }

        public Color TextColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}