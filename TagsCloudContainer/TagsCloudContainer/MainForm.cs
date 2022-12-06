using TagsCloudContainer.Actions;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] actions)
        {
            var menu = new MenuStrip();
            menu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(menu);

            InitializeComponent();
        }
    }
}