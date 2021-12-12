using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.UI.Menu
{
    public class MainMenu
    {
        private readonly Dictionary<int, Category> categories;
        public Category[] Categories => categories.Values.ToArray();

        public MainMenu(Dictionary<int, Category> categories)
        {
            this.categories = categories;
        }

        public Category GetCategory(int key)
        {
            if (categories.ContainsKey(key))
                return categories[key];
            throw new ArgumentException($"No Category with key {key}");
        }
    }
}