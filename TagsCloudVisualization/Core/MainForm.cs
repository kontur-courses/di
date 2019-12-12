using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Common;
using TagsCloudVisualization.Infrastructure.UiActions;

namespace TagsCloudVisualization.Core
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions,
            PictureBoxImageHolder pictureBox,
            ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tags cloud generator";
        }
    }
}