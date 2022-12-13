using System.Drawing;
using TagsCloud.Core.Settings;
using TagsCloud.Core.TagContainersCreators;

namespace TagsCloud.Core.BitmapPainters;

public class DebugPainter : ITagPainter
{
	private readonly ISettingsGetter<ImageSettings> settingsProvider;
	public DebugPainter(ISettingsGetter<ImageSettings> settingsProvider)
	{
		this.settingsProvider = settingsProvider;
	}

	public Bitmap Draw(IEnumerable<TagContainer> tagContainers)
	{
		var imageSettings = settingsProvider.Get();
		var imageSize = GetMinImageSize(tagContainers.ToList(), 100);
		var result = new Bitmap(imageSize.Width, imageSize.Height);

		using var backgroundBrush = new SolidBrush(imageSettings.Pallet.BackgroundColor);
		using var fontBrush = new SolidBrush(imageSettings.Pallet.GetNextColor());
		using var containerBorderPen = new Pen(Color.Crimson);
		using var graphics = Graphics.FromImage(result);

		graphics.FillRectangle(backgroundBrush, imageSize);

		foreach (var tagContainer in tagContainers)
		{
			var place = new Rectangle(new Point(tagContainer.Border.X + imageSize.Width / 2,
				tagContainer.Border.Y + imageSize.Height / 2), tagContainer.Border.Size);
			using var font = new Font(imageSettings.FontFamily, tagContainer.FontSize);

			graphics.DrawString(
				tagContainer.Tag.Word,
				font,
				fontBrush,
				place);

			graphics.DrawRectangle(containerBorderPen, place);

			fontBrush.Color = imageSettings.Pallet.GetNextColor();
		}

		return result;
	}

	private static Rectangle GetMinImageSize(IReadOnlyCollection<TagContainer> tagContainers, int padding = 0)
	{
		var minX = tagContainers.Min(t => t.Border.Left);
		var minY = tagContainers.Min(t => t.Border.Top);

		var maxX = tagContainers.Max(t => t.Border.Right + padding * 2);
		var maxY = tagContainers.Max(t => t.Border.Bottom + padding * 2);

		var width = maxX - minX;
		var height = maxY - minY;

		return new Rectangle(0, 0, width, height);
	}
}