using System.Drawing;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI.UIActions;

namespace TagCloud.Interfaces.GUI
{
    public class MainForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly BaseApplication baseApplication;

        public Image Image { get; private set; }

        public MainForm(IUiAction[] actions, PictureBox pictureBox, BaseApplication baseApplication)
        {
            this.baseApplication = baseApplication;
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            ClientSize = new Size(600, 400);
            this.pictureBox = pictureBox;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Closed += (a, b) => baseApplication.Close();
        }

        public void RedrawImage()
        {
            Image = baseApplication.GetImage();
            pictureBox.Image = Image;
            Controls.Add(pictureBox);
            ClientSize = Image.Size;
            Refresh();
        }
    }
}