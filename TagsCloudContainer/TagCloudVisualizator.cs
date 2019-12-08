using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.FileManager;
using TagsCloudContainer.Filters;
using TagsCloudContainer.RectangleGenerator;
using TagsCloudContainer.TokensGenerator;
using TagsCloudContainer.Visualization;

namespace TagsCloudContainer
{
    public class TagCloudVisualizator
    {
        private readonly IFileManager fileManager;
        private readonly ITokensParser tokensParser;
        private readonly IFilter filter;
        private readonly IRectangleGenerator rectangleGenerator;

        public TagCloudVisualizator(IFileManager fileManager, ITokensParser tokensParser, IFilter filter,
            IRectangleGenerator rectangleGenerator)
        {
            this.fileManager = fileManager;
            this.tokensParser = tokensParser;
            this.filter = filter;
            this.rectangleGenerator = rectangleGenerator;
        }

        public void DrawTagCloud(string inputFile, string outputFile, ICloudSetting setting)
        {
            var text = fileManager.ReadFile(inputFile);

            var strTokens = tokensParser.GetTokens(text);
            strTokens = filter.Filtering(strTokens);

            var tokens = CreateTokens(strTokens).OrderBy(token => token.Count).Reverse();

            var visualizer = new Visualizer(setting);
            var font = setting.Font;
            foreach (var token in tokens)
            {
                var rect = rectangleGenerator.PutNextRectangle(TextRenderer.MeasureText(token.Value, font));
                visualizer.DrawTag(new TagRectangle(token.Value, rect), font);
                if (font.Size >= 9)
                    font = new Font(font.FontFamily, font.Size - 4);
            }

            Console.WriteLine();
            visualizer.Save(outputFile);
        }

        private static IEnumerable<Token> CreateTokens(IEnumerable<string> tokens)
        {
            var countToken = new Dictionary<string, int>();
            foreach (var token in tokens)
            {
                if (countToken.ContainsKey(token))
                {
                    countToken[token]++;
                }
                else
                {
                    countToken[token] = 1;
                }
            }

            return countToken.Select(token => new Token(token.Key, (uint) token.Value));
        }

        private class Token
        {
            public string Value { get; }
            public uint Count { get; }

            public Token(string value, uint count)
            {
                Value = value;
                Count = count;
            }
        }
    }
}