using System.IO;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Solved.Step06.Infrastructure.Injection;
using FractalPainting.Solved.Step06.Infrastructure.UiActions;

namespace FractalPainting.Solved.Step06.App.Actions
{
    public class SaveImageAction : IUiAction, INeed<IImageDirectoryProvider>, INeed<IImageHolder>
    {
        private IImageDirectoryProvider imageDirectoryProvider;
        private IImageHolder imageHolder;

        public void SetDependency(IImageDirectoryProvider dependency)
        {
            imageDirectoryProvider = dependency;
        }

        public void SetDependency(IImageHolder dependency)
        {
            imageHolder = dependency;
        }

        public string Category => "Файл";
        public string Name => "Сохранить...";
        public string Description => "Сохранить изображение в файл";

        public void Perform()
        {
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                InitialDirectory = Path.GetFullPath(imageDirectoryProvider.ImagesDirectory),
                DefaultExt = "bmp",
                FileName = "image.bmp",
                Filter = "Изображения (*.bmp)|*.bmp" 
            };
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
                imageHolder.SaveImage(dialog.FileName);
        }
    }
}