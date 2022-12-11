using System.ComponentModel;

namespace TagCloudGraphicalUserInterface
{
    public class Palette
    {
        [DisplayName("Приоритетный цвет")]
        public Color PrimaryColor { get; set; } = Color.Magenta;
        [DisplayName("Второстепенный цвет")]
        public Color SecondaryColor { get; set; } = Color.Yellow;
        [DisplayName("Цвет фона")]
        public Color BackgroundColor { get; set; } = Color.White;
    }
}
