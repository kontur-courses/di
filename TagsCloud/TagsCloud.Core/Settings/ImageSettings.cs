using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Core.BitmapPainters.Pallets;

namespace TagsCloud.Core.Settings;

public class ImageSettings
{
	public Size ImageSize { get; set; }
	public ITagCLoudPallet Pallet { get; set; }
	public FontFamily FontFamily { get; set; }
	public int MinFontSize { get; set; }
	public ImageFormat Format { get; set; }

	public static ImageSettings GetDefaultSettings()
	{
		return new ImageSettings
		{
			ImageSize = new Size(1280, 720),
			FontFamily = FontFamily.GenericMonospace,
			MinFontSize = 14,
			Format = ImageFormat.Png,
			Pallet = new MonocolorPallet()
		};
	}
}