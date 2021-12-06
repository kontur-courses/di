using System;
using System.Diagnostics;

namespace FractalPainting.App.Actions
{
    public static class CategoryExtensions
    {
        public static string GetString(this Category category)
        {
            return (category) switch
            {
                Category.File => "Файл",
                Category.Fractals => "Фракталы",
                Category.Settings => "Настройки",
                _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
            };
        }
    }
}