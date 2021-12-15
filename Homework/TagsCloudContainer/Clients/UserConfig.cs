using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        public Brush TagsColor { get; private set; }
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
            GetExcludedWords(options.ExcludedWords);
            TryGetTagsColor(options);
            ThrowIfAnyArgIsIncorrect();
        }

        private void GetExcludedWords(IEnumerable<string> words)
        {
            foreach (var word in words)
                ExcludedWords.ExcludeWord(word);
        }

        private void TryGetTagsColor(Options options)
        {
            try
            {
                TagsColor = (Brush) typeof(Brushes).GetProperty(options.Color).GetValue(null, null);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException($"There is no such color \"{options.Color}\"");
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
