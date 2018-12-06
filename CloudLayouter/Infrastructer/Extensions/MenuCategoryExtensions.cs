using System;
using System.ComponentModel;
using CloudLayouter.Infrastructer.Common;

namespace CloudLayouter.Infrastructer.Extensions
{
    public static class MenuCategoryExtensions
    {
        public static string GetDescription(this MenuCategory menuCategory)
        {
            var field = menuCategory.GetType().GetField(menuCategory.ToString());

            var attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;

            return attribute == null ? menuCategory.ToString() : attribute.Description;
        }
    }
}