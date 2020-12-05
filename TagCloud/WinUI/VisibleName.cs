using System;
using System.Collections.Generic;
using TagsCloudVisualisation.Layouting;
using TagsCloudVisualisation.Output;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;
using TagsCloudVisualisation.Text.Preprocessing;

namespace WinUI
{
    public static class VisibleName
    {
        private static readonly Dictionary<Type, string> overridingNames = new Dictionary<Type, string>
        {
            {typeof(CircularTagCloudLayouterFactory), "Circular layout"},
            {typeof(RandomFontSizeResolver), "Random font size"},
            {typeof(BlacklistWordFilter), "Without \"boring\" words"},
            {typeof(MyStemWordsConverter), "Yadnex MyStem"},
            {typeof(LengthWordFilter), "Only with length more or equal to 3"},
            {typeof(LowerCaseConverter), "Lower cased"},
            {typeof(FileWordsReader), "Text file"},
            {typeof(FileResultWriter), "Save to file"},
        };

        public static string Get(Type type) => 
            overridingNames.TryGetValue(type, out var overriden) 
                ? overriden 
                : type.Name;
    }
}