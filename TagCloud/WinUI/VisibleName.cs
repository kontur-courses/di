using System;
using System.Collections.Generic;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Text.Preprocessing;

namespace WinUI
{
    public static class VisibleName
    {
        private static readonly Dictionary<Type, string> overridingNames = new Dictionary<Type, string>
        {
            {typeof(CircularTagCloudLayouter), "Круговая раскладка"},
            {typeof(RandomFontSizeResolver), "Случайный размер шрифта"},
            {typeof(BlacklistWordFilter), "Без \"скучных\" слов"},
            {typeof(MyStemWordsConverter), "Yadnex MyStem"},
            {typeof(LengthWordFilter), "Only with length more or equal to 3"},
        };

        public static string Get(Type type) =>
            overridingNames.TryGetValue(type, out var overriden) ? overriden : type.Name;
    }
}