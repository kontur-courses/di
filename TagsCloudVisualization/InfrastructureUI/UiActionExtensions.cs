using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization.InfrastructureUI
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IEnumerable<IUiAction> actions)
        {
            var items = actions.GroupBy(a => a.Category)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(Category name, IEnumerable<IUiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name.ToString(), null, menuItems);
        }

        public static ToolStripItem ToMenuItem(this IUiAction action)
        {
            return
                new ToolStripMenuItem(action.Name, null, (sender, args) => action.Perform())
                {
                    ToolTipText = action.Description,
                    Tag = action
                };
        }
    }
}