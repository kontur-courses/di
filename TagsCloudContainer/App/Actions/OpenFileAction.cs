using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.App.Layouter;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Actions
{
    public class OpenFileAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly ITagsReader readTags;

        public OpenFileAction(IImageDirectoryProvider imageDirectoryProvider, ITagsReader readTags)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
            this.readTags = readTags;
        }

        public string Category => "Настройки";
        public string Name => "Загрузить текст...";
        public string Description => "Загрузить текст из файла";

        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                Filter = "Изображения (*.txt)|*.txt"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                readTags.ReadTagsFromFile(dialog.FileName);
        }
    }
}