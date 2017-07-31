using System.Drawing;

namespace FractalPainting.Solved.Step11.Infrastructure
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