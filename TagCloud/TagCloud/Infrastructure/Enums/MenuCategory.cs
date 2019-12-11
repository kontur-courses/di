using System.ComponentModel;

namespace TagCloud
{
    public enum MenuCategory
    {
        [Description("File")]
        File = 0,

        [Description("Settings")]
        Settings = 1,

        [Description("Lists")]
        Lists = 2,

        [Description("Paint cloud")]
        CloudPainter = 3
    }
}
