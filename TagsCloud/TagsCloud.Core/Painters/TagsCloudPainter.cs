using System.Drawing;
using TagsCloud.Core.Settings;
using TagsCloud.Core.TagContainersProviders;

namespace TagsCloud.Core.Painters;

public class TagsCloudPainter : ITagsCloudPainter
{
	private readonly ISettingsGetter<ImageSettings> settingsProvider;

	public TagsCloudPainter(ISettingsGetter<ImageSettings> settingsProvider)
	{
		this.settingsProvider = settingsProvider;
	}

	public Bitmap Draw(IEnumerable<TagContainer> tagContainers)
	{
		var imageSettings = settingsProvider.Get();
		var cloudBorder = GetCloudBorder(tagContainers.ToList(), 100);
		var cloud = new Bitmap(cloudBorder.Width, cloudBorder.Height);

		using var backgroundBrush = new SolidBrush(imageSettings.Pallet.BackgroundColor);
		using var fontBrush = new SolidBrush(imageSettings.Pallet.GetNextColor());
		using var containerBorderPen = new Pen(Color.Crimson);
		using var graphics = Graphics.FromImage(cloud);

		graphics.FillRectangle(backgroundBrush, cloudBorder);

		foreach (var tagContainer in tagContainers)
		{
			var place = new Rectangle(new Point(tagContainer.Border.X + cloudBorder.Width / 2,
				tagContainer.Border.Y + cloudBorder.Height / 2), tagContainer.Border.Size);
			using var font = new Font(imageSettings.FontFamily, tagContainer.FontSize);

			graphics.DrawString(
				tagContainer.Tag.Word,
				font,
				fontBrush,
				place);

			fontBrush.Color = imageSettings.Pallet.GetNextColor();
		}

		return new Bitmap(cloud, imageSettings.ImageSize);
	}

	private static Rectangle GetCloudBorder(IReadOnlyCollection<TagContainer> tagContainers, int padding = 0)
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