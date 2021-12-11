using System.IO;
using System.Windows.Forms;
using App.Implementation.SettingsHolders;

namespace GuiClient.UiActions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly OutputResultSettings outputSettingses;

        public SaveImageAction(IImageHolder imageHolder, OutputResultSettings outputSettingses)
        {
            this.imageHolder = imageHolder;
            this.outputSettingses = outputSettingses;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(outputSettingses.OutputFilePath),
                Filter = "Изображения (*.png;*.jpeg;*.bmp)|*.png;*.jpeg;*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                outputSettingses.OutputFilePath = dialog.FileName;
                imageHolder.SaveImage();
            }
        }
    }
}