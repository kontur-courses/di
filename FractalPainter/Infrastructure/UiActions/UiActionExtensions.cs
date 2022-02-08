using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FractalPainting.Infrastructure.UiActions
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IUiAction[] actions)
        {
            var items = actions.GroupBy(a => a.Category).OrderBy(grouping => GetGroupOrder(grouping.Key))
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static int GetGroupOrder(string category)
        {
            switch (category)
            {
                case "Файл":
                    return 1;
                case "Фракталы":
                    return 2;
                case "Настройки":
                    return 3;
                default:
                    throw new InvalidOperationException($"Unknown menu item category {category}");
            }
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IList<IUiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
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