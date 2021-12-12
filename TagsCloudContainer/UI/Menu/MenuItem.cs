using System;

namespace TagsCloudContainer.UI.Menu
{
    public class MenuItem
    {
        public string Name { get; }
        private readonly Action action;

        public MenuItem(string name, Action action)
        {
            Name = name;
            this.action = action;
        }

        public void Perform()
            => action();
    }
}