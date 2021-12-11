using System.Drawing;
using App.Infrastructure.SettingsHolders;

namespace App.Implementation.SettingsHolders
{
    public class FontSettings : IFontSettingsHolder
    {
        public Font Font { get; set; } = new Font("times new roman", 10);
    }
}