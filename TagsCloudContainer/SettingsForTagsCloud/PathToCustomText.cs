using System;
using System.IO;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class PathToCustomText : ICloudParameter
    {
        public ParameterType Type => ParameterType.PathToCustomText;
        public Func<string, object> GetValue => CheckPathToText;

        private object CheckPathToText(string path)
        {
            if (File.Exists(path))
                return path;
            throw new Exception("Doesn't contain the text file");
        }
    }
}