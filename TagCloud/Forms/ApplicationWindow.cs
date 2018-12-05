using System.Drawing;
using System.Windows.Forms;
using TagCloud.Interfaces;
using TagCloud.Settings;

namespace TagCloud.Forms
{
    class ApplicationWindow : Form, IClient
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

        public void RunApplication()
        {
            Application.Run(this);
        }
    }
} 
