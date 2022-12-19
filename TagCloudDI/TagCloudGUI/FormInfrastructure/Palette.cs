using System.ComponentModel;

namespace TagCloudGUI
{
    public class Palette
    {
        [DisplayName("Цвет максимума")]
        public Color PrimaryColor { get; set; } = Color.Magenta;
        [DisplayName("Цвет минимума")]
        public Color SecondaryColor { get; set; } = Color.Yellow;
        [DisplayName("Цвет фона")]
        public Color BackgroundColor { get; set; } = Color.White;
    }
}
