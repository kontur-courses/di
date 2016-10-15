using System;
using FractalPainting.Infrastructure;

namespace FractalPainting.Solved.App.Actions
{
	public class ImageSettingsAction : IUiAction
	{
		private readonly IImageHolder imageHolder;
	    private readonly Lazy<ImageSettings> imageSettings;

	    public ImageSettingsAction(IImageHolder imageHolder,
            Lazy<ImageSettings> imageSettings)
	    {
	        this.imageHolder = imageHolder;
	        this.imageSettings = imageSettings;
	    }

		public string Category => "Настройки";
		public string Name => "Изображение...";
		public string Description => "Размеры изображения";

		public void Perform()
		{
			SettingsForm.For(imageSettings.Value).ShowDialog();
			imageHolder.RecreateImage(imageSettings.Value);
		}
	}
}