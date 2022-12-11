using System.IO;
using System.Windows.Forms;
using TagsCloudContainer.App.Layouter;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Actions
{
    public class OpenFileAction : IUiAction
    {
        private readonly IImageDirectoryProvider imageDirectoryProvider;
        private readonly ITextReader textReader;
        private readonly ITagsExtractor tagsExtractor;

        public OpenFileAction(IImageDirectoryProvider imageDirectoryProvider, ITextReader textReader, ITagsExtractor tagsExtractor)
        {
            this.imageDirectoryProvider = imageDirectoryProvider;
            this.textReader = textReader;
            this.tagsExtractor = tagsExtractor;
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
                Filter = textReader.Filter
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var allText = textReader.ReadText(dialog.FileName);
                tagsExtractor.FindAllTagsInText(allText);
            }
        }
    }
}