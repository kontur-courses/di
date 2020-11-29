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

        private static string GetFilePath(string path)
        {
            var directoryLength = path.LastIndexOf('\\');
            if (Directory.Exists(path.Substring(0, directoryLength)))
                return path;
            throw new Exception("Doesn't contain the directory to save file");
        }
    }
}