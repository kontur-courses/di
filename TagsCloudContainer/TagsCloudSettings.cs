using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Infrastructure.PointTracks;

namespace TagsCloudContainer
{
    public class TagsCloudSettings
    {
        public readonly Font Font;
        public readonly Size ImageSize;
        public readonly IPointsTrack LayoutTrack;
        public readonly int TagContainerPadding;
        public readonly Color TextColor;
        public readonly Color BackgroundColor;
        public readonly HashSet<string> StopWords;

        public TagsCloudSettings(
            Font font = null, 
            Size? imageSize = null,
            IPointsTrack layoutTrack = null, 
            int? tagContainerPadding = null,
            Color? textColor = null, 
            Color? backgroundColor = null, 
            HashSet<string> stopWords = null)
        {   
            Font = font ?? new Font(FontFamily.GenericSansSerif, 60);
            ImageSize = imageSize ?? new Size(1000, 1000);
            LayoutTrack = layoutTrack ?? new SpiralTrack(new Point(ImageSize.Width / 2, ImageSize.Height / 2), 0.01);
            TagContainerPadding = tagContainerPadding ?? 1;
            TextColor = textColor ?? Color.DarkOrange;
            BackgroundColor = backgroundColor ?? Color.Gray;
            StopWords = stopWords ?? new HashSet<string> {"быть", "мочь"};
        }

        public static TagsCloudSettings DefaultSettings => new TagsCloudSettings();
    }
}