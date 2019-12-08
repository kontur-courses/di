using System.Collections.Generic;
using System.Windows.Forms;
using TagCloud.Visualization;
using TagCloudForm.Actions;
using TagCloudForm.Holder;
using TagCloudForm.Settings;

namespace TagCloudForm
{
    public sealed class TagCloudForm : Form
    {
        public TagCloudForm(IEnumerable<IUiAction> actions, PictureBoxImageHolder pictureBox,
            ImageSettings imageSettings)
        {
            ClientSize = imageSettings.ImageSize;
            WindowState = WindowState = FormWindowState.Maximized;
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);

            Text = AppSettings.FormName;

            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
            pictureBox.RecreateImage(imageSettings);
        }
    }
}