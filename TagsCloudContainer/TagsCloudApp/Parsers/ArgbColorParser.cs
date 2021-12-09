using System;
using System.Drawing;

namespace TagsCloudApp.Parsers
{
    public class ArgbColorParser : IArgbColorParser
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