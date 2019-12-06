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
            imageHolder.Image = new Bitmap(1000, 1000, PixelFormat.Format24bppRgb);

            var panel = new FlowLayoutPanel();
            panel.AutoScroll = true;

            panel.Controls.Add(imageHolder);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, menuStrip.Size.Height);
            //panel.AutoScroll = true;
            Controls.Add(menuStrip);
            Controls.Add(panel);
        }
    }
}