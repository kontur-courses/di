using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    class TagCloudCreater
    {
        private string fontFamily;
        private ICloudDrawer cloudDrawer;
        private IFontSizeCalculator fontSizeCalculator;
        private IFileReader fileReader;
        private IImageSaver[] availableImages;
        private IWordsFilter[] wordsFilters;

        public TagCloudCreater(ICloudDrawer cloudDrawer, IFontSizeCalculator fontSizeCalculator, 
            IFileReader fileReader, IImageSaver[] availableImages, IWordsFilter[] wordsFilters)
        {
            this.cloudDrawer = cloudDrawer;
            this.fontSizeCalculator = fontSizeCalculator;
            this.fileReader = fileReader;
            this.wordsFilters = wordsFilters;
            fontFamily = "Arial";
            this.availableImages = availableImages;
        }

        public void Create(string filePath, string targetPath)
        {
            IEnumerable<string> words = fileReader.ReadAllLines(filePath); FilterWords(words);
            var orderedWordsWithFonts = fontSizeCalculator.CalculateFontSize(words, fontFamily)
                .OrderByDescending(word => word.Font.Size).ToList();
            cloudDrawer.DrawCloud(orderedWordsWithFonts, targetPath);
        }

        private List<string> FilterWords(IEnumerable<string> words)
        {
            foreach (var filter in wordsFilters)
            {
                words = filter.Filter(words);
            }

            return words.ToList();
        }

        public void AddStopWord(string stopWord)
        {
            ((StopWordsFilter)wordsFilters[0]).AddStopWord(stopWord);
        }

        public void RemoveStopWord(string stopWord)
        {
            ((StopWordsFilter)wordsFilters[0]).RemoveStopWord(stopWord);
        }

        public void SetFontFamily(string fontFamily)
        {
            this.fontFamily = fontFamily;
        }

        public bool TrySetFontColor(string color)
        {
            try
            {
                cloudDrawer.ColorProvider = new FixedColorProvider(Color.FromName(color));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetFontRandomColor()
        {
            cloudDrawer.ColorProvider = new RandomColorProvider();
        }

        public void SetImageSize(int imageSize)
        {
            cloudDrawer.ChangeImageSize(imageSize);
        }

        public bool TrySetImageFormat(string imageFormat)
        {
            foreach (var imageSaver in availableImages)
            {
                if (imageSaver.Format == imageFormat)
                {
                    cloudDrawer.ImageSaver = imageSaver;
                    return true;
                }
            }

            return false;
        }
    }
}
