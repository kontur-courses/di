using System.Reflection;
using TagsCloudVisualisation.Configuration;

namespace TagsCloudVisualisation.Extensions
{
    public static class RequiredInputExtensions
    {
        public static InputRequestData? GetInputRequestDataOrNull(this ParameterInfo parameterInfo)
        {
            var attr = parameterInfo.GetCustomAttribute<InputRequiredAttribute>();
            if (attr == null)
                return null;
            return new InputRequestData(attr.Description, parameterInfo.ParameterType);
        }
    }
}