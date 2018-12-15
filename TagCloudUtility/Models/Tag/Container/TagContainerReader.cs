using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace TagCloud.Utility.Models.Tag.Container
{
    public class TagContainerReader : ITagContainerReader
    {
        public ITagContainer ReadTagsContainer(string path)
        {
            var text = File.ReadAllText(path);
            var tagContainer = new TagContainer();

            var regex = new Regex(@"(?<groupName>\w+) (?<minCoef>\d[.]\d)-(?<maxCoef>\d[.]\d) (?<fontSize>\d+)");
            var matches = regex.Matches(text);
            if (matches.Count == 0)
                throw new ArgumentException($@"Path {path} didn't contain any group matching regex \w+ \d.\d-\d.\d \d+");
            foreach (Match match in matches)
            {
                if (!match.Success)
                    continue;
                tagContainer.Add(
                    match.Groups["groupName"].Value,
                    new FrequencyGroup(
                        double.Parse(match.Groups["minCoef"].Value, CultureInfo.InvariantCulture),
                        double.Parse(match.Groups["maxCoef"].Value, CultureInfo.InvariantCulture)),
                    int.Parse(match.Groups["fontSize"].Value));
            }

            return tagContainer;
        }
    }
}