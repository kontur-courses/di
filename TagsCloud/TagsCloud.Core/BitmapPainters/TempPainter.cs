using System.Drawing;
using TagsCloud.Core.TagContainersCreators;

namespace TagsCloud.Core.BitmapPainters;

public class TempPainter : ITagPainter
{
	public Bitmap Draw(IEnumerable<TagContainer> tagContainers, Size imageSize)
	{
		var result = new Bitmap(imageSize.Width, imageSize.Height);

		var background = new Rectangle(0, 0, imageSize.Width, imageSize.Height);

		using var backgroundBrush = new SolidBrush(Color.LightSlateGray);
		using var wordBrush = new SolidBrush(Color.Chartreuse);
		using var wordPen = new Pen(Color.Crimson);
		using var graphics = Graphics.FromImage(result);

		graphics.FillRectangle(backgroundBrush, background);

		foreach (var tagContainer in tagContainers)
		{
			graphics.DrawString(
				tagContainer.Tag.Word, 
				new Font(FontFamily.GenericMonospace, tagContainer.Border.Height), 
				wordBrush, 
				tagContainer.Border.X,
				tagContainer.Border.Y);
			graphics.DrawRectangle(wordPen, tagContainer.Border);
		}

		return result;
	}
}