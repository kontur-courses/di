using System.Drawing.Drawing2D;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class ImageSettingsAction : IUiAction, INeed<ImageSettings>, INeed<IImageHolder>
	{
		private ImageSettings imageSettings;
		private IImageHolder imageHolder;
		public string Category => "Настройки";
		public string Name => "Изображение...";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(imageSettings).ShowDialog();
			imageHolder.RecreateImage(imageSettings);
		}

		public void SetDependency(ImageSettings dependency)
		{
			imageSettings = dependency;
		}

		public void SetDependency(IImageHolder dependency)
		{
			imageHolder = dependency;
		}
	}
}