using System.ComponentModel;
using System.Drawing;

namespace TagsCloud
{
    public class Palette
    {
        [DisplayName("Цвет фона")]
        public Color BackgroundColor { get; set; } = Color.FromArgb(35, 35, 35);

        [DisplayName("Цвет текста")]
        public Color TextColor { get; set; } = Color.FromArgb(210, 198, 198);
    }
}