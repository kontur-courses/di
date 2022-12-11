using System.Windows.Forms;
using TagCloud.Readers;
using TagCloudGui.Infrastructure;

namespace TagCloudGui.Actions
{
    public class TextFileReadAction : IUiAction
    {
        private readonly IReader reader;

        public TextFileReadAction(IReader reader)
        {
            this.reader = reader;
        }

        public MenuCategory Category => MenuCategory.File;
        public string Name => "Открыть файл слов";
        public string Description => "Слова должны содержаться в файле, в котором каждое слово начинается с новой строки";

        public void Perform()
        {
            using FileDialog fd = new OpenFileDialog();
            fd.Filter = reader.FileExtFilter;
            if (fd.ShowDialog() == DialogResult.OK)
                reader.SetFile(fd.FileName);
        }
    }
}
