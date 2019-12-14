using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.VisualizerActions.GuiActions;

namespace TagsCloudVisualization.GUI
{
    public static class GuiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IGuiAction[] actions)
        {
            var items = actions.GroupBy(a => a.GetMenuCategory())
                .OrderBy(a => a.Key)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
            return items;
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(MenuCategory category, IList<IGuiAction> items)
        {
            var menuItems = items.Select(a => a.ToMenuItem()).ToArray();
            return new ToolStripMenuItem(GetCategoryDescription(category), null, menuItems);
        }

        private static string GetCategoryDescription(MenuCategory category)
        {
            var fieldInfo = category.GetType().GetField(category.ToString());
            var description = fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description;

            return description ?? category.ToString();
        }

        public static ToolStripItem ToMenuItem(this IGuiAction action)
        {
            return
                new ToolStripMenuItem(action.GetActionName(), null, (sender, args) => action.Perform())
                {
                    ToolTipText = action.GetActionDescription(),
                    Tag = action
                };
        }
    }
}