using System.Linq;
using System.Windows.Forms;

namespace GUITagClouder
{
    public static class UiActionExtensions
    {
        public static ToolStripItem[] ToMenuItems(this IGuiAction[] actions)=>
                actions.GroupBy(a => a.Category)
                    .Select(g => new ToolStripMenuItem(g.Key, null, g.Select(a => a.ToMenuItem()).ToArray()))
                    .Cast<ToolStripItem>()
                    .ToArray();

        public static ToolStripItem ToMenuItem(this IGuiAction action) =>
            new ToolStripMenuItem(action.Name, null, (sender, args) => action.Perform())
            {
                ToolTipText = action.Description
            };
    }
}