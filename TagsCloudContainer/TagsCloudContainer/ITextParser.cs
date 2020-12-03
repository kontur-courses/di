using System.Collections.Generic;

namespace TagsCloudContainer.TagsCloudContainer
{
    public interface ITextParser
    {
        public List<string> GetAllWords(string text);
    }
}