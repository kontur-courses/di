using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using TagsCloud.CloudLayouters;
using TagsCloud.ColorSchemes;
using TagsCloud.Spliters;

namespace TagsCloud
{
    public static class TypesCollector
    {
        private static Dictionary<string, Type> Layouters = new Dictionary<string, Type>
        {
            {"CircularCloud", typeof(CircularCloudLayouter)},
            {"MiddleCloud", typeof(MiddleRectangleCloudLayouter) }
        };

        private static Dictionary<string, Type> Splitter = new Dictionary<string, Type>
        {
            {"Line", typeof(SpliterByLine) },
            {"WhiteSpace", typeof(SpliterByWhiteSpace) }
        };

        private static Dictionary<string, ImageFormat> supportedImageFormat = new Dictionary<string, ImageFormat>
        {
            {"png", ImageFormat.Png },
            {"bmp", ImageFormat.Bmp },
            {"jpeg", ImageFormat.Jpeg }
        };

        private static Dictionary<string, Type> supportedColodScheme = new Dictionary<string, Type>
        {
            {"RandomColor", typeof(RandomColorScheme) },
            {"RedGreenBlueScheme", typeof(RedGreenBlueScheme) }
        };

        public static Type GetTypeGeneationLayoutersByName(string layouterName)
        {
            Type type;
            Layouters.TryGetValue(layouterName, out type);
            return type;
        }

        public static Type GetTypeSpliterByName(string spliterName)
        {
            Type type;
            Splitter.TryGetValue(spliterName, out type);
            return type;
        }

        public static ImageFormat GetFormatFromPathSaveFile(string path)
        {
            var fileInfo = Path.GetFileName(path).Split(".");
            if (fileInfo.Length < 2)
                throw new ArgumentException("Missing image format of result");
            var extension = fileInfo[1].ToLower();
            ImageFormat imageFormat;
            supportedImageFormat.TryGetValue(extension, out imageFormat);
            return imageFormat;
        }

        public static Type GetColorSchemeByName(string schemeName)
        {
            Type colorScheme;
            supportedColodScheme.TryGetValue(schemeName, out colorScheme);
            return colorScheme;
        }
    }
}
