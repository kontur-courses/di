using System.Drawing;
using System.Windows.Forms;

namespace TagCloud
{
    public partial class MainForm : Form
    {
        public MainForm(ImageSettings imageSettings, IVisualizer visualizer)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);
            var pictureBox = new PictureBox
            {
                Image = visualizer.VisualizeTextFromFile("input.txt"),
                ClientSize = new Size(imageSettings.Width, imageSettings.Height)
            };
            Controls.Add(pictureBox);
        }
    }
}
