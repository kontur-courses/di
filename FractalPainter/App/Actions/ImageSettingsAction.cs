using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class ImageSettingsAction : IUiAction
	{
		public ImageSettingsAction(ImageSettings imageSettings, IImageHolder imageHolder)
		{
			this.imageSettings = imageSettings;
			this.imageHolder = imageHolder;
		}

		private readonly ImageSettings imageSettings;
		private readonly IImageHolder imageHolder;
		public string Category => "Настройки";
		public string Name => "Изображение...";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(imageSettings).ShowDialog();
			imageHolder.RecreateImage(imageSettings);
		}

	}
}