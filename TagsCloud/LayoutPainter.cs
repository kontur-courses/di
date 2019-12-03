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
		private readonly ImageSettings _imageSettings;
		private readonly IImageHolder _imageHolder;
		private readonly Palette _palette;
		private readonly FontSettings _fontSettings;

		public LayoutPainter(ImageSettings imageSettings, IImageHolder imageHolder, 
							Palette palette, FontSettings fontSettings)
		{
			_imageSettings = imageSettings;
			_imageHolder = imageHolder;
			_palette = palette;
			_fontSettings = fontSettings;
		}

		public void PaintTags(Layout layout)
		{
			_imageHolder.RecreateImage(_imageSettings);
			var correctTags = layout.Tags
				.Select(t => new Tag(t.Text, t.TextSize, 
					ToComputerCoordinates(t.Area, _imageHolder.GetImageSize())));
			
			var graphics = _imageHolder.GetGraphics();
			var backgroundColor = new SolidBrush(_palette.BackgroundColor);
			graphics.FillRectangle(backgroundColor, 0, 0, _imageSettings.Width, _imageSettings.Height);
			DrawTags(graphics, correctTags);
			_imageHolder.UpdateUi();
		}

		private static Rectangle ToComputerCoordinates(Rectangle rectangle, Size imageSize)
		{
			var xOffset = imageSize.Width / 2;
			var yOffset = -2 * rectangle.Y + imageSize.Height / 2;
			rectangle.Offset(xOffset, yOffset);
			return rectangle;
		}

		private void DrawTags(Graphics graphics, IEnumerable<Tag> tags)
		{
			foreach (var tag in tags)
			{
				var color = _palette.RandomizeColors ? _palette.GenerateColor() : _palette.TextColor;
				graphics.DrawString(tag.Text, _fontSettings.Font, new SolidBrush(color), tag.Area);
			}
		}
	}
}