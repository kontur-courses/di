using System.Collections.Specialized;
using System.Windows.Forms;

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