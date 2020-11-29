using System;
using System.IO;
using TagsCloudContainer.Enums;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.SettingsForTagsCloud
{
    public class PathToSaveCloud : ICloudParameter
    {
        public ParameterType Type => ParameterType.PathToSave;
        public Func<string, object> GetValue => GetFilePath;

        private string GetFilePath(string path)
        {
            var args = path.Split();
            if (Directory.Exists(args[0]))
                return path;
            throw new Exception("Doesn't contain the directory to save file");
        }
    }
}