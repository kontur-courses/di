using TagsCloudContainer.Enums;

namespace TagsCloudContainer.Interfaces
{
    public interface ICloudSettings
    {
        void AddOrUpdateParameter(ParameterType parameterType, string parameterValueFromString);
        void AddOrUpdateParameter<TValue>(ParameterType parameterType, TValue value);
        void AddOrUpdateParameter(string parameterTypeFromString, string parameterValueFromString);
        TValue GetParameterValue<TValue>(ParameterType parameterType);
    }
}