using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface
{
    public static class ActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IActionForm[] actions)
        {
            return actions
                .GroupBy(a => a.Category)
                .Select(g => CreateTopLevelMenuItem(g.Key, g.ToList()))
                .Cast<ToolStripItem>()
                .ToArray();
        }

        private static ToolStripMenuItem CreateTopLevelMenuItem(string name, IEnumerable<IActionForm> items)
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
