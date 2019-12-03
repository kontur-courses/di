using System.IO;
using System.Windows.Forms;
using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class SaveImageAction : IMenuAction
	{
		private IImageHolder _imageHolder;

		public SaveImageAction(IImageHolder imageHolder) => _imageHolder = imageHolder;

		public string Category => "Файл";
		public string Name => "Сохранить раскладку";
		public string Description => "Сохранить текущую раскладку в файл";

		public void Perform()
		{
			var dialog = new SaveFileDialog
			{
				CheckFileExists = false,
				DefaultExt = "png",
				FileName = "image.png",
				Filter = "Изображения|*.png;*.bmp;*.jpg"
			};
			var res = dialog.ShowDialog();
			if (res == DialogResult.OK)
				_imageHolder.SaveImage(dialog.FileName);
		}
	}
}