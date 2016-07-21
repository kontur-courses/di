namespace FractalPainting.App
{
	public interface IImageDirectoryProvider
	{
		string ImagesDirectory { get; }
	}

	public interface IImageSettingsProvider
	{
		ImageSettings ImageSettings { get; }
	}

	public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
	{
		public string ImagesDirectory { get; set; }
		public ImageSettings ImageSettings { get; set; }
	}
}