using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Infrastructure;

namespace TagsCloud.GUI
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBoxImageHolder holder)
        {
            ClientSize = new Size(holder.Settings.Width, holder.Settings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            holder.Dock = DockStyle.Fill;
            holder.RecreateCanvas(holder.Settings);
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
