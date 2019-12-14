using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;
using TagCloudGenerator.Tags;
using TagCloudGenerator.UserInterfaces.CommandLineVerbs;
using TagCloudGenerator.UserInterfaces.VocabularyParsers;

namespace TagCloudGenerator.UserInterfaces
{
    public static class UIDataParser
    {
        private const string WidthGroupName = "width";
        private const string HeightGroupName = "height";

        private static readonly Regex imageSizeValidator =
            new Regex(@"(?<WidthGroupName>\d{3,4})x(?<HeightGroupName>\d{3,4})");

        public static InputData GetInputData(IEnumerable<string> args)
        {
            var options = ReadCommandLineOptions(args);

            VerifyFilename(options);
            
            var cloudVocabulary = ParseCloudVocabulary(options.CloudVocabularyFilename);
            var imageSize = ParseImageSize(options.ImageSize);
            var backgroundColor = ParseColor(options.BackgroundColor);
            var tagStyleByTagType = TagStylesParse(
                options.GroupsCount, options.MutualFont, options.FontSizes, options.TagColors);
            
            return new InputData(cloudVocabulary, imageSize, options.ImageFilename, backgroundColor, tagStyleByTagType);
        }

        private static void VerifyFilename(TagCloudOptions options)
        {
            var invalidCharacterIndex = options.ImageFilename.IndexOfAny(Path.GetInvalidFileNameChars());
            
            if (invalidCharacterIndex > -1) throw new FormatException(
                $@"Filename contains invalid character '{options.ImageFilename[invalidCharacterIndex]}' by index {
                    invalidCharacterIndex}");
        }

        private static TagCloudOptions ReadCommandLineOptions(IEnumerable<string> args)
        {
            TagCloudOptions tagCloudOptions = null;

            Parser.Default.ParseArguments<TagCloudOptions>(args)
                .WithParsed(options => tagCloudOptions = options)
                .WithNotParsed(HandleParseErrors);

            return tagCloudOptions;
        }

        private static IEnumerable<string> ParseCloudVocabulary(string cloudVocabularyFilename)
        {
            var txtVocabularyParser = new TxtVocabularyParser(null); // TODO: DI

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

            foreach (var error in errors) // TODO: Is it clever approach process errors and exit here?
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

        private static Dictionary<TagType, TagStyle> TagStylesParse( // TODO: input should be sorted
            int groupsCount, string mutualFont, string fontSizes, string tagColors)
        {
            var hexColorPattern = new Regex(@"[0-9a-zA-Z]{8}");

            var sizes = new List<int>(groupsCount);
            
            foreach (var sizeString in fontSizes.Split('_'))
                if (int.TryParse(sizeString, out var size) && size > 0 && size < 200)
                    sizes.Add(size);

            var sizesByDescending = sizes.OrderByDescending(size => size).ToArray();

            var colors = (from colorString in tagColors.Split('_')
                          where TryGetFullMatch(hexColorPattern, colorString) != null
                          select ParseColor(colorString))
                .ToList();

            if (sizesByDescending.Length != groupsCount || colors.Count != groupsCount)
                throw new FormatException();

            var tagStyleByTagType = new Dictionary<TagType, TagStyle>(groupsCount);

            for (int i = 0; i < sizesByDescending.Length; i++)
            {
                var font = new Font(mutualFont, sizesByDescending[i]);
                tagStyleByTagType.Add((TagType)i, new TagStyle(colors[i], font));
            }

            return tagStyleByTagType;
        }

        private static Match TryGetFullMatch(Regex pattern, string input)
        {
            var match = pattern.Match(input);

            if (match.Success && match.Length == input.Length)
                return match;
            
            return null;
        }
    }
}