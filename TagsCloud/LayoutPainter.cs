using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System;
using NUnit.Framework.Constraints;
using TagsCloud.Interfaces;

namespace TagsCloud
{
	public class LayoutPainter: ILayoutPainter
	{
		private readonly IImageHolder imageHolder;
		private readonly PainterSettings painterSettings;

		public LayoutPainter(IImageHolder imageHolder, PainterSettings painterSettings)
		{
			this.imageHolder = imageHolder;
			this.painterSettings = painterSettings;
		}

		public void PaintTags(Layout layout)
		{
			var imageSettings = painterSettings.ImageSettings;
			imageHolder.RecreateImage(imageSettings);
			var correctTags = layout.Tags
				.Select(t => new Tag(t.Text, t.TextSize, 
					ToComputerCoordinates(t.Area, imageHolder.GetImageSize())));
			
			var graphics = imageHolder.GetGraphics();
			var backgroundColor = new SolidBrush(painterSettings.Palette.BackgroundColor);
			graphics.FillRectangle(backgroundColor, 0, 0, imageSettings.Width, imageSettings.Height);
			DrawTags(graphics, correctTags);
		}

		internal static Rectangle ToComputerCoordinates(Rectangle rectangle, Size imageSize)
		{
			var xOffset = imageSize.Width / 2;
			var yOffset = -2 * rectangle.Y + imageSize.Height / 2;
			rectangle.Offset(xOffset, yOffset);
			return rectangle;
		}

		private void DrawTags(Graphics graphics, IEnumerable<Tag> tags)
		{
			var palette = painterSettings.Palette;
			foreach (var tag in tags)
			{
				var color = palette.RandomizeColors ? palette.GenerateColor() : palette.TextColor;
				var font = new Font(painterSettings.Font.Font.FontFamily, tag.TextSize);
				graphics.DrawString(tag.Text, font, new SolidBrush(color), tag.Area);
				if (painterSettings.DrawWordRectangle)
					graphics.DrawRectangle(new Pen(color), tag.Area);
				imageHolder.UpdateUi();
			}
		}
	}
}