using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.Core;

public class ImageSaver
{
	private readonly ImageFormat format;
	private readonly string path;

	public ImageSaver(string path, ImageFormat format)
	{
		this.path = path;
		this.format = format;
	}

	public void Save(Image image)
	{
		image.Save(path, format);
	}
}