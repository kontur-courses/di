using System;
using System.Collections.Generic;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Extensions;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class CloudSettings : ICloudSettings
    {
        private Dictionary<ParameterType, Func<string, object>> _parameterValuesCalculating =
            new Dictionary<ParameterType, Func<string, object>>();

        private Dictionary<string, ParameterType> _stringsParameterTypes;
        private Dictionary<ParameterType, object> _parametersAndValues;


        public CloudSettings(List<ICloudParameter> parameters)
        {
            _parametersAndValues = new Dictionary<ParameterType, object>();
            _stringsParameterTypes = new Dictionary<string, ParameterType>();
            foreach (var parameter in parameters)
            {
                _parameterValuesCalculating[parameter.Type] = parameter.GetValue;
                _stringsParameterTypes[parameter.Type.ToString()] = parameter.Type;
            }
        }

        public void AddOrUpdateParameter(ParameterType parameterType, string parameterValueFromString)
        {
            if (_parameterValuesCalculating.TryGetValue(parameterType, out var calculateValue))
                _parametersAndValues[parameterType] = calculateValue(parameterValueFromString);
            else throw new ArgumentException("This parameter is not included in the list of allowed parameters");
        }

        public void AddOrUpdateParameter(string parameterTypeFromString, string parameterValueFromString)
        {
            if (_stringsParameterTypes.TryGetValue(parameterTypeFromString, out var parameterType) &&
                _parameterValuesCalculating.TryGetValue(parameterType, out var calculateValue))
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