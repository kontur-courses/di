using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class SetTextAction : IUiAction
    {
        private readonly string filter;
        private readonly Dictionary<string, IParser> dictionaryParsers;
        private readonly IImageHolder imageHolder;

        public SetTextAction(IImageHolder imageHolder, IEnumerable<IParser> parsers)
        {
            this.imageHolder = imageHolder;
            var pars = parsers as IParser[] ?? parsers.ToArray();

            if (pars.Length == 0)
                throw new ArgumentException("Not found parser");

            dictionaryParsers = pars.ToDictionary(p => p.FileType);

            var types = pars.Select(p => $"*.{p.FileType}").ToArray();
            filter = @$"Текстовые файлы ({string.Join(" ", types)})|{string.Join(";", types)}";
        }

        public string Category => "Файл";
        public string Name => "выбрать...";
        public string Description => "выбрать текстовый файл для создания облака тегов";

        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(Environment.CurrentDirectory),
                Filter = filter
            };
            var res = dialog.ShowDialog();
            if (res != DialogResult.OK) return;

            var index = dialog.FileName.LastIndexOf('.') + 1;
            var fileType = dialog.FileName[index..];
            imageHolder.SetParser(dictionaryParsers[fileType], dialog.FileName);
        }
    }
}