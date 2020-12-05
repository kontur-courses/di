using System;
using System.Collections.Generic;
using TagCloud.Core.Layouting;
using TagCloud.Core.Output;
using TagCloud.Core.Text;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Text.Preprocessing;
using TagCloud.Gui.ImageResizing;

namespace TagCloud.Gui
{
    public static class VisibleName
    {
        private static readonly Dictionary<Type, string> overridingNames = new Dictionary<Type, string>
        {
            {typeof(CircularTagCloudLayouterFactory), "Circular layout"},
            {typeof(RandomFontSizeResolver), "Random font size"},
            {typeof(BiggerAtCenterFontSizeResolver), "Most frequent bigger and closer to center"},
            {typeof(BlacklistWordFilter), "Without \"boring\" words"},
            {typeof(MyStemWordsConverter), "Yadnex MyStem"},
            {typeof(LengthWordFilter), "Only with length more or equal to 3"},
            {typeof(LowerCaseConverter), "Lower cased"},
            {typeof(FileWordsReader), "Text file"},
            {typeof(FileResultWriter), "Save to file"},
            {typeof(DontModifyImageResizer), "Save as it is"},
            {typeof(FitToSizeImageResizer), "Fit to size"},
            {typeof(StretchImageResizer), "Stretch to size"},
            {typeof(PlaceAtCenterImageResizer), "Place at center or fit to size"}
        };

        public static string Get(Type type) => 
            overridingNames.TryGetValue(type, out var overriden) 
                ? overriden 
                : type.Name;
    }
}