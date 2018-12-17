using System;
using System.Drawing;
using System.Windows.Forms;
using TagCloud;

namespace GUITagClouder
{
    public class MainForm : Form
    {
        public MainForm(DrawingSettings settings, CloudHolder cloud, IGuiAction[] actions)
        {
            ClientSize = settings.Size;

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            //cloud.RecreateImage(settings);
            cloud.Dock = DockStyle.Fill;
            Controls.Add(cloud);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud";
        }
    }
}