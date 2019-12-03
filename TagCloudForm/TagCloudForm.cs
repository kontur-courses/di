using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagCloud;
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
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
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