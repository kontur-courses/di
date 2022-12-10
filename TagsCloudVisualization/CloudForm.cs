using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.InfrastructureUI;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class CloudForm : Form
    {
        public CloudForm(IEnumerable<IUiAction> actions, PictureBoxImageHolder pictureBox)
        {
            var imageSettings = new ImageSettings();
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            pictureBox.RecreateImage(imageSettings);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);

            
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Cloud painter";
        }
    }

}