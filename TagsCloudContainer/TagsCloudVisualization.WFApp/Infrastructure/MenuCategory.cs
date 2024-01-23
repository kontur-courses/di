using System.ComponentModel;

namespace TagsCloudVisualization.WFApp.Infrastructure;

public enum MenuCategory
{
    [Description("Файл")]
    File = 0,

    [Description("Облако тегов")]
    TagsClouds = 1,

    [Description("Настройки")]
    Settings = 2,
}