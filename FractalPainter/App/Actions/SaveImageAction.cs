using System.IO;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class SaveImageAction : IUiAction
	{
		public SaveImageAction(IImageDirectoryProvider imagesDirectoryProvider, IImageHolder imageHolder)
		{
			this.imagesDirectoryProvider = imagesDirectoryProvider;
			this.imageHolder = imageHolder;
		}

		private readonly IImageDirectoryProvider imagesDirectoryProvider;
		private readonly IImageHolder imageHolder;

		public string Category => "Файл";
		public string Name => "Сохранить...";
		public string Description => "Сохранить изображение в файл";

		public void Perform()
		{
			var dialog = new SaveFileDialog
			{
				InitialDirectory = Path.GetFullPath(imagesDirectoryProvider.ImagesDirectory),
				DefaultExt = ".png"
			};
			var res = dialog.ShowDialog();
			if (res == DialogResult.OK)
				imageHolder.SaveImage(dialog.FileName);
		}
	}
}