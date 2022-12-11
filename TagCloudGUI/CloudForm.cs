using TagCloudGraphicalUserInterface.Interfaces;
using TagCloudGraphicalUserInterface.Settings;

namespace TagCloudGraphicalUserInterface
{
    public class CloudForm : Form
    {
        public CloudForm(IActionForm[] actionForms, PictureBoxTags pictureBox, ImageSettings imageSettings)
        {

            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(StripMenuItems.ToMenuItems(actionForms));
            Controls.Add(mainMenu);
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);
        }
    }
}
