using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TagCloud.GUI.Settings;

namespace TagCloud.GUI.Extensions
{
    public static class SettingsExtensions
    {
        public static ToolStripItem[] ToMenuItems(this ISettings[] settings, string category)
        {
            var items = settings
                .GroupBy(s => category)
                .Select(g => CreateTopLevelMenuItem(category, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, ICollection<ISettings> items)
        {
            if (items.Count == 1 && name == items.First().GetSettingsName())
            {
                return items.First().ToMenuItem();
            }
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }

        public static ToolStripMenuItem ToMenuItem(this ISettings settings)
        {
            return new ToolStripMenuItem(settings.GetSettingsName(), null, (sender, args) => { SettingsForm.For(settings).ShowDialog(); })
                {
                    Tag = settings
                };
        }
    }
}