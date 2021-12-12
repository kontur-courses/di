using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace TagsCloudContainer.UI.Menu
{
    public class MainMenu
    {
        private readonly Dictionary<int, Category> categories;
        public Category[] Categories => categories.Values.ToArray();

        private TextReader reader;
        private TextWriter writer;

        public MainMenu(Dictionary<int, Category> categories)
        {
            this.categories = categories;
        }

        public void GetCategories()
        {
            writer.WriteLine($"To choose category write position key, for example '1'");
            writer.WriteLine($"To exit write 'Q'");
            foreach (var pos in categories.Keys)
            {
                writer.WriteLine($"{pos}. {categories[pos].Name}");
            }
        }

        public void ChooseCategory(TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            GetCategories();
            while (true)
            {
                var keyStr = reader.ReadLine();
                if (keyStr == "Q")
                    Environment.Exit(0);
                if(int.TryParse(keyStr, out var key))
                    if (categories.ContainsKey(key))
                    {
                        categories[key].ChooseMenuItem(reader, writer);
                        GetCategories();
                    }
                    else
                        writer.WriteLine("No Category with that key");
                else
                    writer.WriteLine("Key should be int!");
            }
        }
    }
}