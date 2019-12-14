using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagCloud.Interfaces.GUI.UIActions
{
    class SaveAction : IUiAction
    {
        private Lazy<MainForm> mainForm;
        public SaveAction(Lazy<MainForm> mainForm)
        {
            this.mainForm = mainForm;
        }
        public string Category => "Файл";
        public string Name => "Сохранить";
        public string Description => "";
        public void Perform()
        {
            if (mainForm.Value.Image is null)
            {
                MessageBox.Show("Для начала необходимо загрузить файл с текстом.", "Ошибка данных",
                    MessageBoxButtons.OK);
                return;
            }

            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                mainForm.Value.Image.Save(dialog.FileName);
        }
    }
}
