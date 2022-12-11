using System.Windows.Forms;
using TagCloud.BoringWordsRepositories;
using TagCloudGui.Infrastructure;

namespace TagCloudGui.Actions
{
    public class BoringWordsReadAction : IUiAction
    {
        private readonly IBoringWordsStorage boringWordsStorage;

        public BoringWordsReadAction(IBoringWordsStorage boringWordsStorage)
        {
            this.boringWordsStorage = boringWordsStorage;
        }
        public MenuCategory Category => MenuCategory.File;
        public string Name => "Загрузить словарь скучных слов";
        public string Description => "Слова должны содержаться в файле, в котором каждое слово начинается с новой строки";

        public void Perform()
        {
            using FileDialog fd = new OpenFileDialog();
            fd.Filter = boringWordsStorage.FileExtFilter;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                boringWordsStorage.LoadBoringWords(fd.FileName);
            }
        }
    }
}
