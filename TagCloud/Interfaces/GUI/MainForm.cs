using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.Interfaces.GUI.UIActions;
using TagCloud.WordsPreprocessing.DocumentParsers;
using TagCloud.WordsPreprocessing.TextAnalyzers;

namespace TagCloud.Interfaces.GUI
{
    public class MainForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly BaseApplication baseApplication;

        public Image Image { get; private set; }

        public MainForm(IUIAction[] actions, PictureBox pictureBox, BaseApplication baseApplication)
        {
            this.baseApplication = baseApplication;
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            ClientSize = new Size(600, 400);
            this.pictureBox = pictureBox;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

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