using System.Windows.Forms;

namespace TagsCloud
{
    public class SaveImageAction : IUiAction
    {
        private IImageHolder imageHolder;
        public SaveImageAction(IImageHolder holder)
        {
            imageHolder = holder;
        }

        public string Category => "Файл";
        public string Name => "Сохранить как...";
        public string Description => "Сохранить изображение в файл";
        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                DefaultExt = "png",
                FileName = "image.png",
                Filter = "Изображения (*.png)|*.png"
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}