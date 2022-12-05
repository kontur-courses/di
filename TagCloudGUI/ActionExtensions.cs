using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagCloudGraphicalUserInterface
{
    public static class ActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IActionForm[] actions)
        {
            var items = actions.GroupBy(a => a.Category)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IList<IActionForm> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }

        public static ToolStripItem ToMenuItem(this IActionForm action)
        {
            return
                new ToolStripMenuItem
                    (action.Name, null, (sender, args) => action.Perform())
                    {
                        ToolTipText = action.Description,
                        Tag = action
                    };
        }
    }
}
