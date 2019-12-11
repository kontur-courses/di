using System.Windows.Forms;
using TagsCloudContainer.ApplicationRunning.UIApp.Forms;

namespace TagsCloudContainer.ApplicationRunning.UIApp
{
    public class UIAppRunner : IAppRunner
    {
        private TagsCloud cloud;
        private SettingsManager settings;
        public UIAppRunner(TagsCloud cloud, SettingsManager settings)
        {
            this.cloud = cloud;
            this.settings = settings;
        }
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm(cloud, settings));
        }
    }
}