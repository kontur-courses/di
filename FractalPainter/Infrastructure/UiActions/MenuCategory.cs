using System;
using System.Collections.Generic;
using System.Text;

namespace FractalPainting.Infrastructure.UiActions
{
    public class MenuCategory
    {

        public string Name { get;}
        public int Order { get;}

        private MenuCategory(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public static MenuCategory File { get; }
        public static MenuCategory Fractals { get; }
        public static MenuCategory Settings { get; }

        static MenuCategory()
        {
            File = new MenuCategory("Файл", 0);
            Fractals = new MenuCategory("Фракталы", 1);
            Settings = new MenuCategory("Настройки", 2);
        }
    }
}
