using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Utility.Models.Tag.Container
{
    public class TagContainer : ITagContainer
    {
        private readonly IDictionary<string, ITagGroup> tags;

        public TagContainer()
        {
            tags = new Dictionary<string, ITagGroup>();
        }

        public void Add(string name, FrequencyGroup frequencyGroup, Size size)
        {
            if (tags.ContainsKey(name))
                throw new ArgumentException($"Group {name} already exist");
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException($"Size can't be negative or zero");
            foreach (var sizeGroup in tags)
            {
                if (sizeGroup.Value.FrequencyGroup.IntersectWith(frequencyGroup))
                    throw new ArgumentException($"Group {name} intersect with {sizeGroup.Key}");
            }

            tags.Add(name, new TagGroup(size, frequencyGroup));
        }

        public void Remove(string groupName)
        {
            if (tags.ContainsKey(groupName))
                tags.Remove(groupName);
        }

        public IEnumerator<ITagGroup> GetEnumerator()
        {
            return tags
                .Select(tagGroup => tagGroup.Value)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}