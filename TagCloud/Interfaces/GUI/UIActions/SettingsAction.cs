using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagCloud.CloudVisualizer.CloudViewConfiguration;
using TagCloud.Interfaces.GUI.Forms;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class SettingsAction : IUIAction
    {
        private CloudViewConfiguration cloudConfiguration;
        private Lazy<MainForm> mainForm;
        private ApplicationSettings applicationSettings;

        public SettingsAction(CloudViewConfiguration cloudConfiguration, Lazy<MainForm> mainForm, ApplicationSettings appSettings)
        {
            this.cloudConfiguration = cloudConfiguration;
            this.mainForm = mainForm;
            applicationSettings = appSettings;
        }

        public string Category => "Настройки";
        public string Name => "Настройки облака";
        public string Description => "";

        public void Perform()
        {
            if (string.IsNullOrEmpty(applicationSettings.FilePath))
            {
                MessageBox.Show("Для начала необходимо загрузить файл с текстом.", "Ошибка данных",
                    MessageBoxButtons.OK);
                return;
            }

            SettingsForm.For(cloudConfiguration).ShowDialog();
            mainForm.Value.RedrawImage();
        }
    }
}
