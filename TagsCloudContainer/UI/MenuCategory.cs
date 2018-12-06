using System.ComponentModel;

namespace TagsCloudContainer.UiActions
{
    public enum MenuCategory
    {
        [Description("Файл")] File = 0,

        [Description("Облако")] TagCloud = 1,

        [Description("Настройки")] Settings = 2
    }
}