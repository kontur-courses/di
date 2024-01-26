using System.ComponentModel;

namespace TagsCloud.Infrastructure.UiActions;

public enum MenuCategory
{
    [Description("Файл")] File = 0,

    [Description("Типы Облаков")] Types = 1,

    [Description("Настойки")] Settings = 2
}