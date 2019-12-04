using System.Drawing;
using System.Windows.Forms;
using TagsCloudForm.Common;
using TagsCloudForm.UiActions;

namespace TagsCloudForm
{
    public class CloudForm : Form
    {

        public CloudForm(IUiAction[] actions, PictureBoxImageHolder pictureBox,
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
    }
}
