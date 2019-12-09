using TagsCloud.Interfaces;

namespace TagsCloud.MenuActions
{
	public class ImageSettingsAction : IMenuAction
	{
		private IImageHolder imageHolder;
		private ImageSettings imageSettings;

		public ImageSettingsAction(IImageHolder imageHolder, ImageSettings imageSettings)
		{
			this.imageHolder = imageHolder;
			this.imageSettings = imageSettings;
		}

		public string Category => "Настройки";
		public string Name => "Размеры изображения";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(imageSettings).ShowDialog();
			imageHolder.RecreateImage(imageSettings);
		}
	}
}