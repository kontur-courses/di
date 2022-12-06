using System.ComponentModel;

namespace TagCloudApp.Infrastructure;

public enum MenuCategory
{
    [Description("File")] File = 0,

    [Description("Tag cloud")] TagCloud = 1,

    [Description("Settings")] Settings = 2,
}