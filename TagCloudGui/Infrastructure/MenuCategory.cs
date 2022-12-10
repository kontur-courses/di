using System.ComponentModel;

namespace TagCloudGui.Infrastructure
{
    public enum MenuCategory
    {
        [Description("Файл")]
        File = 0,

        [Description("Рисунок")]
        Picture = 1,

        [Description("Настройки")]
        Settings = 2,
    }
}