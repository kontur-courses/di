using System.Collections.Generic;

namespace TagsCloudContainer.App.Layouter
{
    public interface ITagsExtractor
    {
        public void FindAllTagsInText(string text);
        public Dictionary<string, int> Text { get; set; }
    }
}
