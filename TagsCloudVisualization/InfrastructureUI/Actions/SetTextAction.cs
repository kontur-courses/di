using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure.Parsers;

namespace TagsCloudVisualization.InfrastructureUI.Actions
{
    public class SetTextAction
    {
        private readonly string filter;

        public SetTextAction(IEnumerable<IParser> parsers)
        {
            var types = parsers.Select(p => $"*.{p.FileType}").ToArray();
            filter = @$"Текстовые файлы ({string.Join(" ", types)})|{string.Join(";", types)}";
        }

        public OpenFileDialog FileDialog()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath("..//..//..//texts"),
                Filter = filter
            };
            return dialog;
        }
    }
}