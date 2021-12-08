using System;
using System.Drawing;

namespace TagsCloudApp
{
    public class ArgbColorParser : IObjectParser<Color>
    {
        public Color Parse(string value)
        {
            try
            {
                return ColorTranslator.FromHtml(value);
            }
            catch (Exception e) when (e is FormatException or ArgumentException)
            {
                throw new ApplicationException($"Incorrect color: {value}");
            }
        }
    }
}