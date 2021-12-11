using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.Infrastructure.SettingsHolders;
using GuiClient.UiActions;

namespace GuiClient
{
    public class MainForm : Form
    {
        public MainForm(
            IEnumerable<IUiAction> actions,
            PictureBoxImageHolder pictureBox,
            IImageSizeSettingsHolder imageSizeSettings)
        {
            ClientSize = imageSizeSettings.Size;
            MaximizeBox = false;
            MinimizeBox = false;

            SetMainMenu(actions);
            SetPictureBox(pictureBox);
        }

        private void SetMainMenu(IEnumerable<IUiAction> actions)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);
        }

        private void SetPictureBox(PictureBoxImageHolder pictureBox)
        {
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }
    }
}