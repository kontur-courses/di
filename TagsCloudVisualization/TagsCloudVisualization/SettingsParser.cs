using System;
using System.Collections.Generic;
using System.Drawing;
using DocoptNet;
using TagsCloudVisualization.ImageSaver;
using TagsCloudVisualization.Providers.Layouter.Interfaces;
using TagsCloudVisualization.Providers.Sizable;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    internal class SettingsParser
    {
        public static ApplicationSettings GetSettings(IDictionary<string, ValueObject> parameters)
        {
            var readerSettings = GetReaderSettings(parameters["--input"].ToString(),
                parameters["--max_words"].ToString(),
                parameters["--exclude"].ToString());


            var drawerSettings = GetDrawerSettings(parameters["--brush"].ToString(),
                parameters["--back"].ToString(),
                parameters["--font"].ToString(),
                parameters["--font_size"].ToString(),
                parameters["--height"].ToString(),
                parameters["--width"].ToString(),
                parameters["--sizer"].ToString());
            var layouterSettings = GetLayouterSettings(parameters["--x"].ToString(),
                parameters["--y"].ToString(),
                parameters["--coef"].ToString(),
                parameters["--type"].ToString());

            var imageExt = (ImageExt) Enum.Parse(typeof(ImageExt),
                parameters["--output_ext"].ToString());
            var savePath = parameters["--output_path"].ToString();
            savePath = string.IsNullOrEmpty(savePath) ? PathHelper.ResourcesPath : savePath;
            return new ApplicationSettings(imageExt, readerSettings, layouterSettings, drawerSettings, savePath);
        }

        private static LayouterSettings GetLayouterSettings(string x, string y, string spiralCoefficient,
            string spiralType)
        {
            int.TryParse(x, out var xInt);
            int.TryParse(y, out var yInt);
            var sType = (SpiralType) Enum.Parse(typeof(SpiralType), spiralType);
            if (!int.TryParse(spiralCoefficient, out var spiralCoefficientInt))
                spiralCoefficientInt = 1;
            return new LayouterSettings(new Point(xInt, yInt), spiralCoefficientInt, sType);
        }

        private static ReaderSettings GetReaderSettings(string textDirectory, string maxObjectCount,
            string badWordsDirectory)
        {
            int.TryParse(maxObjectCount, out var count);
            badWordsDirectory =
                string.IsNullOrEmpty(badWordsDirectory)
                    ? PathHelper.ResourcesPath + "\\BadWords.txt"
                    : badWordsDirectory;
            textDirectory = string.IsNullOrEmpty(textDirectory)
                ? PathHelper.ResourcesPath + "\\HarryPotter.txt"
                : textDirectory;
            return new ReaderSettings(textDirectory, count, badWordsDirectory);
        }

        private static DrawerSettings GetDrawerSettings(string brushColorName, string backGroundColorName,
            string fontName, string fontSize, string imageHeight, string imageWidth, string sizer)
        {
            if (!int.TryParse(fontSize, out var size))
                throw new ArgumentException("Cant parse fontSize");
            if (!int.TryParse(imageHeight, out var height))
                throw new ArgumentException("Cant parse height");
            if (!int.TryParse(imageWidth, out var width))
                throw new ArgumentException("Cant parse width");

            var brushColor = Color.FromName(brushColorName);
            var backGroundColor = Color.FromName(backGroundColorName);

            var textBrush = new SolidBrush(brushColor);
            var font = new Font(fontName, size);

            var sizerType = (SizeSelectorType) Enum.Parse(typeof(SizeSelectorType), sizer);

            return new DrawerSettings(textBrush, backGroundColor, font, height, width, sizerType);
        }
    }
}