using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.Utility.Models.Tag.Container
{
    public class TagContainerReader : ITagContainerReader
    {
        public ITagContainer ReadTagsContainer(string path)
        {
            var text = File.ReadAllText(path);
            var tagContainer = new TagContainer();

            var regex = new Regex(@"\w+ \d.\d-\d.\d \d+");
            var matches = regex.Matches(text);
            if(matches.Count == 0)
                throw new ArgumentException($@"Path {path} didn't contain any group matching regex \w+ \d.\d-\d.\d \d+");
            foreach (Match match in matches)
            {
                if (!match.Success)
                    continue;
                var group = match.Value;
                var items = group.Split(' ');
                var name = items[0];
                var freq = items[1]
                    .Split('-')
                    .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                    .ToArray();
                var size = int.Parse(items[2]);
                tagContainer.Add(name, new FrequencyGroup(freq[0], freq[1]), size);
            }

            return tagContainer;
        }
    }
}
