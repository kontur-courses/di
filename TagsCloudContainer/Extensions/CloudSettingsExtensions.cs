using System;
using TagsCloudContainer.Enums;
using TagsCloudContainer.SettingsForTagsCloud;

namespace TagsCloudContainer.Extensions
{
    public static class CloudSettingsExtensions
    {
        public static object GetDefaultSettingsParameter(this ParameterType type)
        {
            try
            {
                return typeof(DefaultCloudSettings).GetField(type.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Doesn't contain parameter with the type");
            }
        }
    }
}