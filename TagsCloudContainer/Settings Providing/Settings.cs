using System.Collections.Generic;
using System.Drawing;
using Autofac;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer.Settings_Providing
{
    public class Settings
    {
        public readonly string InputPath;
        public readonly string OutputPath;
        public readonly HashSet<string> ExcludedWords;
        public readonly HashSet<string> ExcludedPartsOfSpeech;
        public readonly ColoringOptions ColoringOptions;
        public readonly Size Resolution;
        public readonly string FontName;


        public Settings(
            string inputPath,
            string outputPath,
            ColoringOptions coloringOptions,
            HashSet<string> excludedWords,
            HashSet<string> excludedPartsOfSpeech,
            Size resolution,
            string fontName)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
            ColoringOptions = coloringOptions;
            ExcludedWords = excludedWords;
            ExcludedPartsOfSpeech = excludedPartsOfSpeech;
            Resolution = resolution;
            FontName = fontName;
        }
    }
}