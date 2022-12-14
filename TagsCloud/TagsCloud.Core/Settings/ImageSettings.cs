using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Core.Painters.Pallets;

namespace TagsCloud.Core.Settings;

public class ImageSettings
{
	public Size ImageSize { get; set; }
	public ITagCLoudPallet Pallet { get; set; }
	public FontFamily FontFamily { get; set; }
	public int MinFontSize { get; set; }
	public ImageFormat Format { get; set; }
}