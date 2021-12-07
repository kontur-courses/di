using System;
using System.Drawing;

namespace TagsCloudApp
{
    public class ArgbColorParser : IObjectParser<Color>
    {
        private static ApplicationException GenerateIncorrectColorException(string value) =>
            new ApplicationException($"Incorrect color: {value}");

        public Color Parse(string value)
        {
            var colorParts = value.Split(' ');
            if (colorParts.Length != 4)
                throw GenerateIncorrectColorException(value);

            var colorBytes = new byte[4];
            for (var i = 0; i < colorBytes.Length; i++)
            {
                if (!byte.TryParse(colorParts[i], out var colorByte))
                    throw GenerateIncorrectColorException(value);

                colorBytes[i] = colorByte;
            }

            return Color.FromArgb(colorBytes[0], colorBytes[1], colorBytes[2], colorBytes[3]);
        }
    }
}