using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.Infrastructure.SettingsHolders;
using GuiClient.UiActions;

namespace GuiClient
{
    public class MainForm : Form
    {
        private readonly MenuStrip mainMenu;

        public MainForm(
            IEnumerable<IUiAction> actions,
            PictureBoxImageHolder pictureBox,
            IImageSizeSettingsHolder imageSizeSettings)
        {
            ClientSize = imageSizeSettings.Size;
            MaximizeBox = false;
            MinimizeBox = false;
            mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);
            pictureBox.RecreateImage();
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }
    }
}