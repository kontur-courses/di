using System.IO;

namespace TagsCloudContainer.Settings
{
    public class DefaultSourceFileSettings : ISourceFileSettings
    {
        public string FilePath { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "1984_lines.txt");
    }
}