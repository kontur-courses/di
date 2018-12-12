using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TagCloud.Utility.Models.Tag.Container
{
    public class TagContainerReader : ITagContainerReader
    {
        public ITagContainer ReadTagsContainer(string path)
        {
            var text = File.ReadAllText(path);
            var tagContainer = new TagContainer();
            var groups = text
                .Split(';','\r','\n')
                .Where(line => !string.IsNullOrEmpty(line));

            foreach (var group in groups)
            {
                var items = group.Split(' ');
                var name = items[0];
                var freq = items[1]
                    .Split('-')
                    .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                    .ToArray();
                var size = items[2]
                    .Split('x')
                    .Select(int.Parse)
                    .ToArray();
                tagContainer.Add(name, new FrequencyGroup(freq[0], freq[1]), new Size(size[0], size[1]));
            }

            return tagContainer;
        }
    }
}
