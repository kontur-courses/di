using System;
using System.Drawing;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class TextFont : ICloudParameter
    {
        public ParameterType Type => ParameterType.Font;
        public Func<string, object> GetValue => GetFontFromString;

        private static Font GetFontFromString(string fontFromString)
        {
            var parameters = fontFromString.Split('_');
            if (parameters.Length < 2 || !float.TryParse(parameters[1], out var fontSize) ||
                !TryConvertToFont(parameters[0], fontSize, out var font))
                throw new Exception("Doesn't contain font with the arguments");
            return font;
        }

        private static bool TryConvertToFont(string fontName, float fontSize, out Font font)
        {
            try
            {
                font = new Font(fontName, fontSize);
                return true;
            }
            catch (Exception)
            {
                font = default;
                return false;
            }
        }
    }
}