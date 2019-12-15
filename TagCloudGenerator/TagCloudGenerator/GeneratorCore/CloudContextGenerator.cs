using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloudGenerator.Clients;
using TagCloudGenerator.Clients.VocabularyParsers;
using TagCloudGenerator.GeneratorCore.CloudLayouters;
using TagCloudGenerator.GeneratorCore.Tags;

namespace TagCloudGenerator.GeneratorCore
{
    public class CloudContextGenerator
    {
        private const string WidthGroupName = "width";
        private const string HeightGroupName = "height";

        private static readonly Regex imageSizeValidator =
            new Regex($@"(?<{WidthGroupName}>\d{{3,4}})x(?<{HeightGroupName}>\d{{3,4}})");

        private static readonly Regex hexColorPattern = new Regex(@"#[0-9a-fA-F]{8}");

        private readonly IClient client;

        public CloudContextGenerator(IClient client) => this.client = client;

        public TagCloudContext GetTagCloudContext()
        {
            var cloudOptions = client.GetOptions();

            VerifyFilename(cloudOptions.ImageFilename);

            var cloudVocabulary = ParseCloudVocabulary(cloudOptions.CloudVocabularyFilename).ToArray();
            var excludedWords = ParseCloudVocabulary(cloudOptions.ExcludedWordsVocabularyFilename).ToHashSet();
            var imageSize = ParseImageSize(cloudOptions.ImageSize);
            var backgroundColor = ParseColor(cloudOptions.BackgroundColor);
            var tagStyleByTagType = TagStylesParse(
                cloudOptions.GroupsCount, cloudOptions.MutualFont, cloudOptions.FontSizes, cloudOptions.TagColors);
            var imageCenter = new Point(imageSize.Width / 2, imageSize.Height / 2);
            var tagCloud = cloudOptions.ConstructCloud(backgroundColor, tagStyleByTagType);

            return new TagCloudContext(cloudOptions.ImageFilename,
                                       imageSize,
                                       cloudVocabulary,
                                       tagCloud,
                                       new CircularCloudLayouter(imageCenter),
                                       excludedWords);
        }

        private static void VerifyFilename(string filename)
        {
            var invalidCharacterIndex = filename.IndexOfAny(Path.GetInvalidFileNameChars());

            if (invalidCharacterIndex > -1)
                throw new FormatException(
                    $@"Filename contains invalid character '{filename[invalidCharacterIndex]}' by index {
                        invalidCharacterIndex}");
        }

        private static IEnumerable<string> ParseCloudVocabulary(string cloudVocabularyFilename)
        {
            if (cloudVocabularyFilename is null)
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