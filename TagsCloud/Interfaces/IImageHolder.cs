using System.Drawing;

namespace TagsCloud.Interfaces
{
	public interface IImageHolder
	{
		Size GetImageSize();
		Graphics GetGraphics();
		void UpdateUi();
		void RecreateImage(ImageSettings settings);
		void SaveImage(string fileName);
	}
}