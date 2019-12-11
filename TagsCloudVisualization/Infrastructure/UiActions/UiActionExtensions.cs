using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloudVisualization.Infrastructure.UiActions
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IUiAction[] actions)
        {
            return actions.GroupBy(a => a.Category)
                .OrderBy(a => a.Key)
                .Select(g => CreateToplevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
        }

        private static ToolStripMenuItem CreateToplevelMenuItem(MenuCategory category, IEnumerable<IUiAction> items)
        {
            return new ToolStripMenuItem(
                category.GetDescription(),
                null,
                items.Select(a => a.ToMenuItem()).ToArray());
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