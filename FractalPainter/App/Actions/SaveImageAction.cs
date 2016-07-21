using System.IO;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class SaveImageAction : IUiAction, INeed<IImageDirectorySettings>, INeed<IImageHolder>
	{
		private IImageDirectorySettings imageSettings;
		private IImageHolder imageHolder;
		public string Category => "Файл";
		public string Name => "Сохранить...";
		public string Description => "Сохранить изображение в файл";

		public void Perform()
		{
			var dialog = new OpenFileDialog
			{
				CheckFileExists = false,
				Multiselect = false,
				InitialDirectory = Path.GetFullPath(imageSettings.ImagePath)
				
			};
			var res = dialog.ShowDialog();
			if (res == DialogResult.OK)
				imageHolder.SaveImage(dialog.FileName);
		}

		public void SetDependency(IImageDirectorySettings dependency)
		{
			imageSettings = dependency;
		}

		public void SetDependency(IImageHolder dependency)
		{
			imageHolder = dependency;
		}
	}

}