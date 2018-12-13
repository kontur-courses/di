using System;
using System.Collections;
using System.Collections.Generic;
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

        public void Add(string name, FrequencyGroup frequencyGroup, int fontSize)
        {
            if (tags.ContainsKey(name))
                throw new ArgumentException($"Group {name} already exist");
            if (fontSize <= 0)
                throw new ArgumentException($"Font size can't be negative or zero, but was {fontSize}");
            foreach (var sizeGroup in tags)
            {
                if (sizeGroup.Value.FrequencyGroup.IntersectWith(frequencyGroup))
                    throw new ArgumentException($"Group {name} intersect with {sizeGroup.Key}");
            }

            tags.Add(name, new TagGroup(fontSize, frequencyGroup));
        }

        public void Remove(string groupName)
        {
            if (tags.ContainsKey(groupName))
                tags.Remove(groupName);
        }

        public ITagGroup GetTagGroupFor(double frequencyCoef)
        {
            return tags
                .Values
                .FirstOrDefault(group => group.Contains(frequencyCoef));
        }

        public IEnumerator<(string, ITagGroup)> GetEnumerator()
        {
            return tags
                .Select(tagGroup => (tagGroup.Key,tagGroup.Value))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}