using System.Drawing;
using System.Windows.Forms;
using TagCloud.Settings;

namespace TagCloud
{
    class ApplicationWindow : Form
    {
        public ApplicationWindow(IUiAction[] actions, ImageBox imageBox, ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            imageBox.Dock = DockStyle.Fill;
            Controls.Add(imageBox);
        }
    }
} 
