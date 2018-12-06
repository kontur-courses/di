using System.ComponentModel;

namespace CloudLayouter.Infrastructer.Common
{
    public enum MenuCategory
    {
        [Description("Сохранить")] Save = 1,
        [Description("Настройки")] Settings = 2,
        [Description("Визуализация")] DrawTagCloud = 3
    }
}