using System;
using System.Collections.Generic;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class CloudSettings : ICloudSettings
    {
        private static Dictionary<ParameterType, Func<string, object>> _parameterValuesCalculating =
            new Dictionary<ParameterType, Func<string, object>>();

        private Dictionary<ParameterType, object> _parametersAndValues;

        public CloudSettings(List<ICloudParameter> parameters)
        {
            _parametersAndValues = new Dictionary<ParameterType, object>();
            foreach (var parameter in parameters)
                _parameterValuesCalculating[parameter.Type] = parameter.GetValue;
        }

        public void AddOrUpdateParameter(ParameterType parameterType, string parameterValueFromString)
        {
            if (_parameterValuesCalculating.TryGetValue(parameterType, out var calculateValue))
                _parametersAndValues[parameterType] = calculateValue(parameterValueFromString);
            else throw new ArgumentException("This parameter is not included in the list of allowed parameters");
        }

        public void AddOrUpdateParameter<TValue>(ParameterType parameterType, TValue value)
        {
            if (_parameterValuesCalculating.ContainsKey(parameterType))
                _parametersAndValues[parameterType] = value;
            else throw new ArgumentException("This parameter is not included in the list of allowed parameters");
        }

        public TValue GetParameterValue<TValue>(ParameterType parameterType)
        {
            return _parametersAndValues.ContainsKey(parameterType)
                ? (TValue) _parametersAndValues[parameterType]
                : (TValue) parameterType.GetDefaultSettingsParameter();
        }
    }
}