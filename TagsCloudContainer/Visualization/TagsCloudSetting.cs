using System;
using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public class TagsCloudSetting : ICloudSetting
    {
        public Font Font { get; }
        public Size ImageSize { get; }
        public Color TextColor { get; }
        public Color BackgroundColor { get; }
        public Point GetCenterImage()
        {
            return new Point(ImageSize.Width / 2, ImageSize.Height / 2);
        }

        private TagsCloudSetting(Font font, Size imageSize, Color textColor, Color backgroundColor)
        {
            Font = font;
            ImageSize = imageSize;
            TextColor = textColor;
            BackgroundColor = backgroundColor;
        }
        
        public TagsCloudSetting(ArgumentParser.Options argument)
        {
            Font = new Font(FontFamily.GenericMonospace,argument.FontSize);
            ImageSize = new Size(argument.Size,argument.Size);
            TextColor = GetTextColor(argument.TextColor);
            BackgroundColor = GetBackgroundColor(argument.BackgroundColor);
        }
        
        private static Color GetBackgroundColor(int number)
        {
            switch (number)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.White;
                default:
                    throw new ArgumentException();
            }
        }
        
        private static Color GetTextColor(int number)
        {
            switch (number)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return Color.Black;
                case 2:
                    return Color.White;
                default:
                    throw new ArgumentException();
            }
        } 


        public static TagsCloudSetting GetDefault()
        {
            return new TagsCloudSetting(new Font(FontFamily.GenericMonospace,60), new Size(1000,1000), Color.Red,Color.Black);
        }
    }
}