using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class OpenAction : IUIAction
    {
        public string Category => "Файл";
        public string Name => "Открыть текстовый документ";
        public string Description => "Открывает текстовый документ на чтение";
        public void Perform()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp"

            };
            dialog.ShowDialog();
        }
    }
}
