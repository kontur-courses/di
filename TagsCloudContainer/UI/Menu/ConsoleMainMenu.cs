using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.UI.Menu
{
    public class ConsoleMainMenu : IMainMenu
    {
        private readonly Dictionary<int, ConsoleCategory> categories;
        public ConsoleCategory[] Categories => categories.Values.ToArray();

        private TextReader reader;
        private TextWriter writer;

        public ConsoleMainMenu(Dictionary<int, ConsoleCategory> categories, TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.categories = categories;
        }

        private void GetCategories()
        {
            writer.WriteLine($"To choose category write position key, for example '1'");
            writer.WriteLine($"To exit write 'Q'");
            foreach (var pos in categories.Keys)
            {
                writer.WriteLine($"{pos}. {categories[pos].Name}");
            }
        }

        public void ChooseCategory()
        {
            GetCategories();
            while (true)
            {
                var keyStr = reader.ReadLine();
                if (keyStr == "Q")
                    Environment.Exit(0);
                if (int.TryParse(keyStr, out var key))
                    if (categories.ContainsKey(key))
                    {
                        categories[key].ChooseMenuItem();
                        GetCategories();
                    }
                    else
                        writer.WriteLine("No ConsoleCategory with that key");
                else
                    writer.WriteLine("Key should be int!");
            }
        }
    }
}