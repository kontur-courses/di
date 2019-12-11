using System.ComponentModel;

namespace TagsCloudVisualization.Infrastructure.UiActions
{
    public enum MenuCategory
    {
        [Description("Файл")]
        File = 0,
        
        [Description("Облако")]
        Cloud = 1,

        [Description("Настройки")]
        Settings = 2
    }
}