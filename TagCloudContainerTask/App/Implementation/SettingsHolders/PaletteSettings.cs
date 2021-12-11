using System.Drawing;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class PaletteSettings : IPaletteSettingsHolder
    {
        public Color WordColor { get; set; } = Color.Black;

        public Color BackgroundColor { get; set; } = Color.White;
    }
}