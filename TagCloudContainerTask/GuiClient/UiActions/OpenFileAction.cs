using System.Windows.Forms;
using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    internal class OpenFileAction : IUiAction
    {
        private readonly InputFileSettings settings;

        public OpenFileAction(InputFileSettings settings)
        {
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Открыть...";
        public string Description => "Использовать файл как источник слов";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Текстовые файлы (*.txt;*.docx)|*.txt;*.docx"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                settings.InputFileName = dialog.FileName;
        }
    }
}