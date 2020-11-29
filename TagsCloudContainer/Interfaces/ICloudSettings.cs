using TagsCloudContainer.Enums;

namespace TagsCloudContainer.Interfaces
{
    public interface ICloudSettings
    {
        void AddOrUpdateParameter(ParameterType parameterType, string parameterValueFromString);
        void AddOrUpdateParameter<TValue>(ParameterType parameterType, TValue value);
        TValue GetParameterValue<TValue>(ParameterType parameterType);
    }
}