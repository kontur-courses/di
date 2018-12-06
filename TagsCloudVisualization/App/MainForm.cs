using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.App.Actions;
using TagsCloudVisualization.TagsCloud;
using TagsCloudVisualization.TagsCloud.CircularCloud;
using TagsCloudVisualization.WordProcessing;

namespace TagsCloudVisualization.App
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, TagsCloudSettings tagCloudSettings, PictureBoxImageHolder imageHolder)
        {
            ClientSize = new Size(800,600);
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            imageHolder.RecreateImage(tagCloudSettings);
            imageHolder.Dock = DockStyle.Fill;
            Controls.Add(imageHolder);
        }
    }
}