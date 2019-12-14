using System;
using System.Windows.Forms;
using TagCloud.Interfaces.GUI.Forms;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class ApplicationSettingsAction : IUiAction
    {
        private Lazy<MainForm> mainForm;
        private ApplicationSettings applicationSettings;
        private Form applicationSettingsDialog;

        public ApplicationSettingsAction(Lazy<MainForm> mainForm,
            ApplicationSettings appSettings, ApplicationSettingsForm appSettingsDialog)
        {
            applicationSettingsDialog = appSettingsDialog;
            this.mainForm = mainForm;
            applicationSettings = appSettings;
        }

        public string Category => "Настройки";
        public string Name => "Конфигурация препроцессинга слов";
        public string Description => "";

        public void Perform()
        {
            if (string.IsNullOrEmpty(applicationSettings.FilePath))
            {
                MessageBox.Show("Для начала необходимо загрузить файл с текстом.", "Ошибка данных",
                    MessageBoxButtons.OK);
                return;
            }

            applicationSettingsDialog.ShowDialog();

            mainForm.Value.RedrawImage();
        }
    }
}
