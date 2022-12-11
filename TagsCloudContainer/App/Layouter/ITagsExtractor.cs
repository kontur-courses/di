using System.Collections.Generic;

namespace TagsCloudContainer.App.Layouter
{
    public interface ITagsExtractor
    {
        public Dictionary<string, int> FindAllTagsInText(string text);
    }
}
