using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudContainer.UI.Menu
{
    public class Category
    {
        private readonly Dictionary<int, MenuItem> items;
        private TextReader reader;
        private TextWriter writer;
        public MenuItem[] Items => items.Values.ToArray();
        public string Name { get; }

        public Category(Dictionary<int, MenuItem> items, string name)
        {
            this.items = items;
            Name = name;
        }

        public void GetMenuItems()
        {
            writer.WriteLine($"To choose Menu action write position key, for example '1'");
            writer.WriteLine($"To return to main menu write 'Q'");
            foreach (var pos in items.Keys)
            {
                writer.WriteLine($"{pos}. {items[pos].Name}");
            }
        }

        public void ChooseMenuItem(TextReader reader, TextWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            GetMenuItems();
            while (true)
            {
                var keyStr = reader.ReadLine();
                if (keyStr == "Q")
                    return;
                if (int.TryParse(keyStr, out var key))
                    if (items.ContainsKey(key))
                    {
                        items[key].Perform();
                        GetMenuItems();
                    }
                    else
                        writer.WriteLine("No menu item with that key");
                else
                    writer.WriteLine("Key should be int!");
            }
        }
    }
}