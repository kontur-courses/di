using TagsCloudContainer.Common;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
{
    internal class FilesSettingsAction : IUiAction
    {
        private readonly FilesSettings filesSettings;

        public FilesSettingsAction(FilesSettings filesSettings)
        {
            this.filesSettings = filesSettings;
        }

        public string Category => "Настройки";
        public string Name => "Файлы...";
        public string Description => "Файлы";

        public void Perform()
        {
            SettingsForm.For(filesSettings).ShowDialog();
        }
    }
}