using System.Drawing;

namespace TagsCloudVisualization.AppSettings
{
    public class Palette
    {
        public Color TextColor { get; set; } = Color.Red;
        public Color BackgroundColor { get; set; } = Color.Black;

        public bool EnabledMultipleColors { get; set; } = true;
        public bool DrawTextBoundingZone { get; set; } = false;

        public Color[] Colors { get; set; } = 
        {
            Color.MediumVioletRed, Color.Orange, Color.White,
            Color.Red, Color.LawnGreen, Color.SpringGreen
        };
    }
}