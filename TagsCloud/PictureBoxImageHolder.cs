using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class PictureBoxImageHolder : PictureBox, IImageHolder
	{
		public Size GetImageSize()
		{
			FailIfNotInitialized();
			return Image.Size;
		}

		public Graphics GetGraphics()
		{
			FailIfNotInitialized();
			return Graphics.FromImage(Image);
		}

		private void FailIfNotInitialized()
		{
			if (Image == null)
				throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
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
			FailIfNotInitialized();
			Image.Save(fileName);
		}
	}
}