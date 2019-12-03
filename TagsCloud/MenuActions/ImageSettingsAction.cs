using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class ImageSettingsAction : IMenuAction
	{
		private IImageHolder _imageHolder;
		private ImageSettings _imageSettings;

		public ImageSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings)
		{
			_imageHolder = imageHolder;
			_imageSettings = imageSettings;
		}

		public string Category => "Настройки";
		public string Name => "Размеры изображения";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(_imageSettings).ShowDialog();
			_imageHolder.RecreateImage(_imageSettings);
		}
	}
}