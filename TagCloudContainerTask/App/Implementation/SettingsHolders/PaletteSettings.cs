using System.Drawing;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class PaletteSettings : IPaletteSettingsHolder
    {
        public Color WordColor { get; set; }

        public Color BackgroundColor { get; set; }
    }
}