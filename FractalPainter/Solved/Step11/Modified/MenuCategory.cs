using System.ComponentModel;

namespace FractalPainting.Solved.Step11.Modified
{
    public enum MenuCategory
    {
        [Description("Файл")]
        File = 0,

        [Description("Фракталы")]
        Fractals = 1,

        [Description("Настройки")]
        Settings = 2,
    }
}