using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using FractalPainting.Infrastructure;

namespace FractalPainting.Solved.App
{
	public class PictureBoxImageHolder : PictureBox, IImageHolder
	{
		public Size GetImageSize()
		{
			return Image.Size;
		}

		public Graphics StartDrawing()
		{
			return Graphics.FromImage(Image);
		}

		public void UpdateUi()
		{
			Refresh();
			Application.DoEvents();
		}

		public void RecreateImage(ImageSettings imageSettings)
		{
			Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
		}

		public void SaveImage(string fileName)
		{
			Image.Save(fileName);
		}
	}
}