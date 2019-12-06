using System.ComponentModel;

namespace FractalPainting.Solved.Step11.Infrastructure.UiActions
{
    public enum MenuCategory
    {
        [Description("����")]
        File = 0,

        [Description("��������")]
        Fractals = 1,

        [Description("���������")]
        Settings = 2,
    }
}