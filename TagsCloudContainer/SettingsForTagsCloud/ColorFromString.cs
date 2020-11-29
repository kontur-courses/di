using System;
using System.Drawing;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public static class ColorFromString
    {
        public static object GetColorFromString(string name)
        {
            try
            {
                return Color.FromName(name);
            }
            catch (Exception e)
            {
                throw new Exception("Doesn't contain color with the name", e);
            }
        }
    }
}