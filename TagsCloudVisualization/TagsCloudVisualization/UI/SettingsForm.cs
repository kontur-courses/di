using System.Windows.Forms;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization
{
    public class SettingsForm : Form
    {
        public SettingsForm(ImageSettings imageSettings)
        {
            Controls.Add(new PropertyGrid
            {
                SelectedObject = imageSettings,
                Dock = DockStyle.Fill
            });
        }
    }
}