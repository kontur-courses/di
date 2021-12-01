using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace FractalPainting.Infrastructure.UiActions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            return fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString();
        }
    }
}
