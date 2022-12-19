using TagCloudGUI.Interfaces;

namespace TagCloudGUI
{
    public static class StripMenuItems
    {
        public static ToolStripItem[] ToMenuItems(IActionForm[] actions)
        {
            var groupingActions = GroupActionByCategory(actions);
            var menuItems = CreateMenuItems(groupingActions);
            return menuItems.ToArray();
        }

        private static IEnumerable<IGrouping<string, IActionForm>> GroupActionByCategory(IEnumerable<IActionForm> actionForms)
        {
            return actionForms.GroupBy(action => action.Category);
        }

        private static IEnumerable<ToolStripItem> CreateMenuItems(IEnumerable<IGrouping<string, IActionForm>> groupingActions)
        {
            return groupingActions
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()));
        }

        public static ToolStripMenuItem CreateTopLevelMenuItem(string name, IEnumerable<IActionForm> items)
        {
            var menuItems = items.Select(ToMenuItem).ToArray();
            return new ToolStripMenuItem(name, null, menuItems);
        }
        public static ToolStripItem ToMenuItem(IActionForm action)
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
