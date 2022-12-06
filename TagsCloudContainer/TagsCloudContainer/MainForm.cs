using System.Drawing.Printing;
using System.Windows.Forms;
using TagsCloudContainer.Actions;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] actions, ImageHolder holder, ImageSettings imageSettings)
        {
            Size = new Size(imageSettings.Width, imageSettings.Height);

            var menu = new MenuStrip();
            menu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(menu);
            
            holder.RecreateImage(imageSettings);
            holder.Dock = DockStyle.Fill;
            Controls.Add(holder);

            InitializeComponent();
        }
    }
}