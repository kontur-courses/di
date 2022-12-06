using System.ComponentModel;

namespace TagCloudApp;

public enum MenuCategory
{
    [Description("Файл")] File = 0,

    [Description("Облако тегов")] TagCloud = 1,

    [Description("Настройки")] Settings = 2,
}