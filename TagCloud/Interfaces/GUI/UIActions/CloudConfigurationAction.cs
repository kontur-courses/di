using System;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI.Forms;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class CloudConfigurationAction : IUiAction
    {
        private ApplicationSettings appSettings;
        private Lazy<MainForm> mainForm;
        private CloudSettingsForm cloudSettingsForm;

        public CloudConfigurationAction(ApplicationSettings appSettings,
            CloudSettingsForm form, Lazy<MainForm> mainForm)
        {
            this.appSettings = appSettings;
            this.mainForm = mainForm;
            cloudSettingsForm = form;
        }

        public string Category => "Настройки";
        public string Name => "Конфигураия облака";
        public string Description => "";
        public void Perform()
        {
            if(string.IsNullOrEmpty(appSettings.FilePath))
            {
                MessageBox.Show("Для начала необходимо загрузить файл с текстом.", "Ошибка данных",
                    MessageBoxButtons.OK);
                return;
            }

            cloudSettingsForm.ShowDialog();

            mainForm.Value.RedrawImage();
        }
    }
}
