using System.IO;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.SourceTextReaders
{
    public class TxtSourceTextReader : ISourceTextReader
    {
        private readonly ISourceFileSettings settings;

        public TxtSourceTextReader(ISourceFileSettings settings)
        {
            this.settings = settings;
        }
        
        public string[] ReadText()
        {
            return File.ReadAllLines(settings.FilePath);
        }
    }
}
