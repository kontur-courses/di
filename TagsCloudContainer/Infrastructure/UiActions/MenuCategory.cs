using System.ComponentModel;

namespace TagsCloudContainer.Infrastructure.UiActions
{
    public enum MenuCategory
    {
        [Description("Алгоритмы")] 
        Algorithms = 0,

        [Description("Настройки")]
        Settings = 1
    }
}