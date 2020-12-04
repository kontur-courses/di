using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace TagsCloud
{
    public class RenderFileAction : IUiAction
    {
        public RenderFileAction(IWordsFrequencyParser parser, IImageHolder holder, ICloudLayouter layouter)
        {
            this.parser = parser;
            this.holder = holder;
            this.layouter = layouter;
        }

        private IWordsFrequencyParser parser;
        private IImageHolder holder;
        private ICloudLayouter layouter;
        public string Category => "Файл";
        public string Name => "Визуализировать файл...";
        public string Description => "Выбрать файл для визуализации облака тегов";
        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Path.GetFullPath(Assembly.GetExecutingAssembly().Location),
                DefaultExt = "txt",
                Filter = "Текстовые файлы (*.txt) | *.txt"
            };

            var res = dialog.ShowDialog();

            if (res != DialogResult.OK) 
                return;
            holder.RecreateCanvas(holder.Settings);
            layouter.ClearLayouter();
            var frequencies = parser.ParseWordsFrequency(dialog.FileName);
            holder.RenderWords(frequencies);
        }
    }
}