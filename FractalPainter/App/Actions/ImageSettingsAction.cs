using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class ImageSettingsAction : IUiAction, INeed<IImageSettingsProvider>, INeed<IImageHolder>
	{
		private IImageSettingsProvider imageSettingsProvider;
		private IImageHolder imageHolder;
		public string Category => "Настройки";
		public string Name => "Изображение...";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			var imageSettings = imageSettingsProvider.ImageSettings;
			SettingsForm.For(imageSettings).ShowDialog();
			imageHolder.RecreateImage(imageSettings);
		}

		public void SetDependency(IImageSettingsProvider dependency)
		{
			imageSettingsProvider = dependency;
		}

		public void SetDependency(IImageHolder dependency)
		{
			imageHolder = dependency;
		}
	}
}