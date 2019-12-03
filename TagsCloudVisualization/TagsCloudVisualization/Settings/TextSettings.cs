using System.Collections.Generic;
using System.IO;

using TagsCloudVisualization.Filters;

namespace TagsCloudVisualization.Settings
{
    class TextSettings
    {
        private readonly Dictionary<string, IFilter> filters = new Dictionary<string, IFilter>()
        {
            {"POS", new PartOfSpeachFilter()}
        };

        public string PathToFile { get; private set; }
        public string FileExtention { get; private set; }
        public IFilter Filter { get; private set; }

        public TextSettings(string path, string filter)
        {
            PathToFile = path;
            FileExtention = Path.GetExtension(path);
            Filter = filters[filter];
        }
    }
}
