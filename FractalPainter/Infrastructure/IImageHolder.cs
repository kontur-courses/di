using System.Drawing;
using FractalPainting.App;

namespace FractalPainting.Infrastructure
{
	public interface IImageHolder
	{
		Size GetImageSize();
		Graphics StartDrawing();
		void UpdateUi();
		void RecreateImage(ImageSettings settings);
		void SaveImage(string fileName);
	}
}