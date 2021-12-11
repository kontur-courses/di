using System.Drawing;

namespace App.Infrastructure.SettingsHolders
{
    public interface IPaletteSettingsHolder
    {
        Color WordColor { get; }
        Color BackgroundColor { get; }
    }
}