using TagCloudGraphicalUserInterface.Interfaces;

namespace TagCloudGraphicalUserInterface
{
    public class CloudForm : Form
    {
        public CloudForm(IActionForm[] actionForms, PictureBoxTags pictureBox, ImageSettings imageSettings)
        {

            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actionForms.ToMenuItems());
            Controls.Add(mainMenu);
            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pictureBox);
        }
    }
}
