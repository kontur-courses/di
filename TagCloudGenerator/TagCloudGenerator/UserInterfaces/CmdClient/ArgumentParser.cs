using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using TagCloudGenerator.CloudLayouters;
using TagCloudGenerator.TagClouds;
using TagCloudGenerator.Tags;
using TagCloudGenerator.UserInterfaces.CmdClient.CommandLineVerbs;
using TagCloudGenerator.UserInterfaces.VocabularyParsers;

namespace TagCloudGenerator.UserInterfaces.CmdClient
{
    public static class ArgumentParser
    {
        private const string WidthGroupName = "width";
        private const string HeightGroupName = "height";

        private static readonly Regex imageSizeValidator =
            new Regex($@"(?<{WidthGroupName}>\d{{3,4}})x(?<{HeightGroupName}>\d{{3,4}})");

        private static readonly Regex hexColorPattern = new Regex(@"#[0-9a-zA-Z]{8}");

        public static TagCloudContext GetTegCloudContext(IEnumerable<string> args)
        {
            var options = ReadCommandLineOptions(args);

            VerifyFilename(options.ImageFilename);

            var cloudVocabulary = ParseCloudVocabulary(options.CloudVocabularyFilename).ToArray();
            var excludedWords = ParseCloudVocabulary(options.ExcludedWordsVocabularyFilename).ToHashSet();
            var imageSize = ParseImageSize(options.ImageSize);
            var backgroundColor = ParseColor(options.BackgroundColor);
            var tagStyleByTagType = TagStylesParse(
                options.GroupsCount, options.MutualFont, options.FontSizes, options.TagColors);
            var imageCenter = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var tagCloud = options is DoubleFontsCloud
                               ? new CommonWordsCloud(backgroundColor, tagStyleByTagType)
                               : (TagCloud<TagType>)new WebCloud(backgroundColor, tagStyleByTagType);

            return new TagCloudContext(options.ImageFilename,
                                       imageSize,
                                       cloudVocabulary,
                                       tagCloud,
                                       new CircularCloudLayouter(imageCenter),
                                       excludedWords);
        }

        private static void VerifyFilename(string filename) // TODO: null argument
        {
            var invalidCharacterIndex = filename.IndexOfAny(Path.GetInvalidFileNameChars());

            if (invalidCharacterIndex > -1)
                throw new FormatException(
                    $@"Filename contains invalid character '{filename[invalidCharacterIndex]}' by index {
                        invalidCharacterIndex}");
        }

        private static TagCloudOptions ReadCommandLineOptions(IEnumerable<string> args)
        {
            TagCloudOptions tagCloudOptions = null;

            Parser.Default.ParseArguments<DoubleFontsCloud, FourFontsCloud>(args)
                .WithParsed<DoubleFontsCloud>(options => tagCloudOptions = options)
                .WithParsed<FourFontsCloud>(options => tagCloudOptions = options)
                .WithNotParsed(HandleParseErrors);

            return tagCloudOptions;
        }

        private static IEnumerable<string> ParseCloudVocabulary(string cloudVocabularyFilename)
        {
            if (cloudVocabularyFilename is null) // TODO: is it ok?
                return Enumerable.Empty<string>();

            var txtVocabularyParser = new TxtVocabularyParser(null);

            return txtVocabularyParser.GetCloudVocabulary(cloudVocabularyFilename);
        }

        private static Size ParseImageSize(string imageSize)
        {
            var match = imageSizeValidator.Match(imageSize);

            if (!(match.Success && match.Length == imageSize.Length))
                throw new FormatException("Invalid image size format.");

            var width = int.Parse(match.Groups[WidthGroupName].Value);
            var height = int.Parse(match.Groups[HeightGroupName].Value);

            return new Size(width, height);
        }

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            var exitCode = 0;

            foreach (var error in errors)
            {
                Console.Error.WriteLine(error);
                exitCode = (int)error.Tag;
            }

            Environment.Exit(exitCode);
        }

        private static Color ParseColor(string hexColor)
        {
            var argbColor = int.Parse(hexColor.Replace("#", ""), NumberStyles.HexNumber);

            return Color.FromArgb(argbColor);
        }

        private static Dictionary<TagType, TagStyle> TagStylesParse(
            int groupsCount, string mutualFont, string fontSizes, string tagColors)
        {
            var sizes = ParseSizes(fontSizes).ToArray();
            var colors = ParseColors(tagColors).ToArray();

            if (sizes.Length != groupsCount || colors.Length != groupsCount)
                throw new FormatException($"Sizes count has to be equal to colors count and GroupsCount={groupsCount}");

            var tagStyleByTagType = Enumerable.Range(0, groupsCount)
                .ToDictionary(i => (TagType)i,
                              i =>
                              {
                                  var font = new Font(mutualFont, sizes[i]);
                                  return new TagStyle(colors[i], font);
                              });

            return tagStyleByTagType;
        }

        private static IEnumerable<Color> ParseColors(string tagColors) =>
            tagColors.Split('_')
                .Where(colorString => IsFullMatch(hexColorPattern, colorString))
                .Select(ParseColor);

        private static IEnumerable<int> ParseSizes(string fontSizes)
        {
            foreach (var sizeString in fontSizes.Split('_'))
                if (int.TryParse(sizeString, out var size) && size > 0 && size < 200)
                    yield return size;
        }

        private static bool IsFullMatch(Regex pattern, string input)
        {
            var match = pattern.Match(input);

            return match.Success && match.Length == input.Length;
        }
    }
}