using System;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class BoringWords : ICloudParameter
    {
        public ParameterType Type => ParameterType.BoringWords;
        public Func<string, object> GetValue => GetArrayBoringWordsFromString;

        private static object GetArrayBoringWordsFromString(string boringWordsFromString)
        {
            if (string.IsNullOrEmpty(boringWordsFromString))
                throw new Exception("Doesn't contain custom boring words");
            return boringWordsFromString.Split('_');
        }
    }
}