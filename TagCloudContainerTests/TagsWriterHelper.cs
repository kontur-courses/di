using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Common;

namespace TagCloudContainerTests
{
    public class TagsWriterHelper
    {
        public static void Write(string path, IEnumerable<SimpleTag> tags)
        {
            var serializer = new Serializer();
            var storage = new FileBlobStorage();
            var str = GetTagsToString(tags);
            var data = serializer.Serialize(str);
            storage.Set(path, data);
        }

        public static string GetTagsToString(IEnumerable<SimpleTag> tags)
            => string.Join("\r\n", GetWords(tags));

        private static IEnumerable<string> GetWords(IEnumerable<SimpleTag> tags)
            => tags.SelectMany(tag => tag.GetWords());
    }
}