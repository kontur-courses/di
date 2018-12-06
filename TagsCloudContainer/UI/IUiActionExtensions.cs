using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudContainer.UI
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IUiAction[] actions)
        {
            var items = actions.GroupBy(a => a.Category)
                .OrderBy(a => a.Key)
                .Select(g => CreateToplevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateToplevelMenuItem(MenuCategory category, IList<IUiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(category.GetDescription(), null, menuItems);
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