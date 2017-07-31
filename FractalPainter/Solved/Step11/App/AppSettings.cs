using FractalPainting.Solved.Step11.Infrastructure;

namespace FractalPainting.Solved.Step11.App
{
	public class AppSettings : IImageDirectoryProvider, IImageSettingsProvider
	{
		public string ImagesDirectory { get; set; }
		public ImageSettings ImageSettings { get; set; }
	}
}