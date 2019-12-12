using System.Windows.Forms;
using TagsCloudGenerator.ConsoleUI;
using TagsCloudVisualization.Core.Painter;
using TagsCloudVisualization.Infrastructure.Common;
using TagsCloudVisualization.Infrastructure.UiActions;

namespace TagsCloudVisualization.Core.Actions
{
    public class SaveImageAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly TagCloudSettings tagCloudSettings;

        public SaveImageAction(
            IImageHolder imageHolder,
            TagCloudSettings tagCloudSettings)
        {
            this.imageHolder = imageHolder;
            this.tagCloudSettings = tagCloudSettings;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                DefaultExt = tagCloudSettings.ImageExtension,
                FileName = $"image.{tagCloudSettings.ImageExtension}",
                Filter = $"Изображения (*.{tagCloudSettings.ImageExtension})|*.{tagCloudSettings.ImageExtension}"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                imageHolder.SaveImage(dialog.FileName,
                    ImageFormatUtils.GetImageFormatByExtension(tagCloudSettings.ImageExtension));
            }
        }
    }
}