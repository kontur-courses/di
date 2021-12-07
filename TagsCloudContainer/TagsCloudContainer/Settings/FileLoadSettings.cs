using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class FileLoadSettings : IFileLoadSettings
    {
        public FileLoadSettings(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}