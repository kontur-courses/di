using System.ComponentModel;

namespace GuiClient
{
    public enum MenuCategory
    {
        [Description("Файл")] File = 1,

        [Description("Настройки")] Settings = 2,

        [Description("Отрисовка")] Redraw = 3
    }
}