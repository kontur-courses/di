using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class OpenFileAction : IUiAction
    {
        private readonly IImageHolder imageHolder;

        public OpenFileAction(IImageHolder imageHolder)
        {
            this.imageHolder = imageHolder;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Открыть...";
        public string Description => "Использовать файл как источник тегов";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                InitialDirectory = Path.GetFullPath(imageHolder.GetAppSettings().InputFileName),
                Filter = "Текстовые файлы (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.GetAppSettings().InputFileName = dialog.FileName;
        }
    }
}