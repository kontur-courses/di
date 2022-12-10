using System.Collections.Generic;

namespace TagsCloudContainer.App.Layouter
{
    public interface ITagsReader
    {
        public void ReadTagsFromFile(string path);
        public Dictionary<string, int> Text { get; set; }
    }
}
