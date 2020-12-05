using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Infrastructure;

namespace TagsCloud.GUI
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBoxImageHolder holder, ImageSettings settings)
        {
            ClientSize = new Size(settings.Width, settings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            holder.Dock = DockStyle.Fill;
            holder.RecreateCanvas(settings);
            Controls.Add(holder);
            holder.UpdateUi();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud Visualizer";
        }
    }
}
