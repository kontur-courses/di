using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Utility.Models
{
    public class TagGroups
    {
        public readonly Dictionary<string, TagGroup> Groups;

        public TagGroups()
        {
            Groups = new Dictionary<string, TagGroup>();
        }

        public void AddSizeGroup(string name, FrequencyGroup frequencyGroup, Size size)
        {
            if (Groups.ContainsKey(name))
                throw new ArgumentException($"Group {name} already exist");
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException($"Size can't be negative or zero");
            foreach (var sizeGroup in Groups)
            {
                if (sizeGroup.Value.FrequencyGroup.IntersectWith(frequencyGroup))
                    throw new ArgumentException($"Group {name} intersect with {sizeGroup.Value}");
            }

            Groups.Add(name, new TagGroup(size, frequencyGroup));
        }

        public void DeleteGroup(string groupName)
        {
            if (Groups.ContainsKey(groupName))
                Groups.Remove(groupName);
        }
    }
}