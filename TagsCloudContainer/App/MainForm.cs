using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App
{
    public class MainForm : Form
    {
        public MainForm(IEnumerable<IUiAction> actions,
            PictureBoxImageHolder pictureBox)
        {
            ClientSize = new Size(pictureBox.GetAppSettings().ImageSettings.Width,
                pictureBox.GetAppSettings().ImageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(pictureBox.GetAppSettings());
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "TagsCloud Painter";
        }
    }
}