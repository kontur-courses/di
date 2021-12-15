﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagsCloudContainer.PaintConfigs;
using TagsCloudContainer.TextParsers;

namespace TagsCloudContainer.Clients
{
    public class UserConfig
    {
        public string InputFile { get; private set; }
        public string OutputFile { get; private set; }
        public Size ImageSize { get; private set; }
        public Point ImageCenter { get; private set; }
        public string TagsFontName { get; private set; }
        public int TagsFontSize { get; private set; }
        public ITextFormatReader FormatReader { get; private set; }
        public IColorScheme TagsColors { get; private set; }
        public BoringWords ExcludedWords { get; private set; }

        public UserConfig()
        {
            ExcludedWords = new BoringWords();
        }

        public void GetConfig(Options options)
        {
            InputFile = options.Input;
            OutputFile = options.Output;
            ImageSize = new Size(options.Width, options.Height);
            ImageCenter = new Point(ImageSize.Width / 2, ImageSize.Height / 2);
            TagsFontName = options.FontName;
            TagsFontSize = options.FontSize;
            FormatReader = GetTextFormatReader(options.InputFileFormat);
            GetExcludedWords(options.ExcludedWords);
            TagsColors = TryGetTagsColors(options);
            ThrowIfAnyArgIsIncorrect();
        }

        private ITextFormatReader GetTextFormatReader(string formatName)
        {
            if (formatName.ToLower() != "txt")
                throw new ArgumentException("Unknown input file format!");
            return new TxtReader();
        }

        private void GetExcludedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                ExcludedWords.AddWord(word.ToLower());
        }

        private IColorScheme TryGetTagsColors(Options options)
        {
            switch (options.Color)
            {
                case 0: return new BlackWhiteScheme();
                case 1: return new CamouflageScheme();
                default: throw new ArgumentException("Unknow color scheme number!");
            }
        }

        private void ThrowIfAnyArgIsIncorrect()
        {
            if (!File.Exists(InputFile))
                throw new ArgumentException("There is no such file!");
            if (ImageSize.Width <= 0 || ImageSize.Height <= 0)
                throw new ArgumentException("Image size is incorrect!");
            if (TagsFontSize <= 0)
                throw new ArgumentException("Font size is incorrect!");
            var font = new Font(TagsFontName, TagsFontSize);
            if (font.Name != TagsFontName)
                throw new ArgumentException("Font name is incorrect!");
        }
    }
}
