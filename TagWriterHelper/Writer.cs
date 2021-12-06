using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagWriterHelper
{
    public static class Writer
    {
        public static void Write(string path, IEnumerable<Tag> tags)
        {
            var serializer = new Serializer();
            var storage = new FileBlobStorage();
            var str = GetTagsToString(tags);
            var data = serializer.Serialize(str);
            storage.Set(path, data);
        }

        private static string GetTagsToString(IEnumerable<Tag> tags) 
            => string.Join("\n", GetWords(tags));

        private static IEnumerable<string> GetWords(IEnumerable<Tag> tags)
            => tags.SelectMany(tag => tag.GetWords());
    }
}