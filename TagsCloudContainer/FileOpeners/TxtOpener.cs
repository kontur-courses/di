using System.IO;

namespace TagsCloudContainer.FileOpeners
{
    public class TxtOpener : IFileOpener
    {
        private string FilePath { get; }
        public TxtOpener(CustomSettings settings) => FilePath = settings.PathToOpen;
        public string OpenFile() => File.ReadAllText(FilePath);
    }
}