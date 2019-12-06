using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Actions;

namespace TagsCloudVisualization
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] menuActions, PictureBox imageHolder)
        {
            var menuStrip = new MenuStrip();
            foreach (var action in menuActions)
            {
                var button = new ToolStripButton(action.Name, null, (sender, args) => action.Perform());
                menuStrip.Items.Add(button);
            }
            imageHolder.Dock = DockStyle.Fill;
            imageHolder.SizeMode = PictureBoxSizeMode.AutoSize;
            var panel = new FlowLayoutPanel {AutoScroll = true};
            panel.Controls.Add(imageHolder);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, menuStrip.Size.Height);
            Controls.Add(menuStrip);
            Controls.Add(panel);
        }
    }
}