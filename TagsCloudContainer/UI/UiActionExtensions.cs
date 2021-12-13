using System.Collections.Generic;
using System.IO;
using TagsCloudContainer.UI.Menu;

namespace TagsCloudContainer.UI
{
    public static class UiActionExtensions
    {
        public static ConsoleMainMenu GetConsoleMenu
            (this ConsoleUiAction[] actions, TextReader reader, TextWriter writer)
        {
            var categories = GetCategories(actions, reader, writer);
            var menuCategories = new Dictionary<int, ConsoleCategory>();
            for (var i = 0; i < categories.Length; i++) 
                menuCategories[i + 1] = categories[i];
            return new ConsoleMainMenu(menuCategories, reader, writer);
        }

        private static ConsoleCategory[] GetCategories
            (ConsoleUiAction[] actions, TextReader reader, TextWriter writer)
        {
            var result = new List<ConsoleCategory>();
            var categories = new Dictionary<string, List<MenuItem>>();
            
            foreach (var uiAction in actions)
            {
                var category = uiAction.Category;
                var menuItem = CreateMenuItem(uiAction);
                if (!categories.ContainsKey(category))
                    categories[category] = new List<MenuItem>();
                categories[category].Add(menuItem);
            }
            
            foreach (var name in categories.Keys)
            {
                var categoryItems = new Dictionary<int, MenuItem>();
                var itmes = categories[name];
                for (var i = 0; i < itmes.Count; i++) 
                    categoryItems[i + 1] = itmes[i];
                result.Add(new ConsoleCategory(categoryItems, name, reader, writer));
            }

            return result.ToArray();
        }

        private static MenuItem CreateMenuItem(ConsoleUiAction action) 
            => new MenuItem(action.Name, action.Perform);
    }
}