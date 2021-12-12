using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.UI.Menu
{
    public class Category
    {
        private readonly Dictionary<int, MenuItem> items;
        public MenuItem[] Items => items.Values.ToArray();
        public string Name { get; }

        public Category(Dictionary<int, MenuItem> items, string name)
        {
            this.items = items;
            Name = name;
        }

        public MenuItem GetMenuItem(int key)
        {
            if (items.ContainsKey(key))
                return items[key];
            throw new ArgumentException($"No Item with key {key}");
        }
    }
}