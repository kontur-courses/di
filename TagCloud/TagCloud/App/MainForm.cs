using System.Drawing;
using System.Windows.Forms;

namespace TagCloud
{
    public partial class MainForm : Form
    {
        public MainForm(IUiAction[] actions, ImageHolder pictureBox, ImageSettings imageSettings)
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
