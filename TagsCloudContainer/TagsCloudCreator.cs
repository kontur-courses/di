using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagsCloudCreator
    {
        public string FontFamily { get; set; }
        private ICloudDrawer cloudDrawer;
        private IFontSizeCalculator fontSizeCalculator;
        private IFileReader fileReader;
        private IImageSaver[] availableImages;
        private IWordsFilter[] wordsFilters;

        public TagsCloudCreator(ICloudDrawer cloudDrawer, IFontSizeCalculator fontSizeCalculator, 
            IFileReader fileReader, IImageSaver[] availableImages, IWordsFilter[] wordsFilters)
        {
            this.cloudDrawer = cloudDrawer;
            this.fontSizeCalculator = fontSizeCalculator;
            this.fileReader = fileReader;
            this.wordsFilters = wordsFilters;
            this.availableImages = availableImages;
            FontFamily = "Arial";
            this.cloudDrawer.ColorProvider = new FixedColorProvider(Color.Black);
        }

        public void Create(string filePath, string targetPath, string imageName)
        {
            IEnumerable<string> words = fileReader.ReadAllLines(filePath); 
            words = FilterWords(words);
            var orderedWordsWithFonts = fontSizeCalculator.CalculateFontSize(words, FontFamily)
                .OrderByDescending(word => word.Font.Size).ToList();
            cloudDrawer.DrawCloud(orderedWordsWithFonts, targetPath, imageName);
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
            foreach (var wordsFilter in wordsFilters)
                if (wordsFilter is StopWordsFilter)
                    ((StopWordsFilter)wordsFilter).AddStopWord(stopWord);
        }

        public void RemoveStopWord(string stopWord)
        {
            foreach (var wordsFilter in wordsFilters)
                if (wordsFilter is StopWordsFilter)
                    ((StopWordsFilter)wordsFilter).RemoveStopWord(stopWord);
        }

        public bool TrySetFontFamily(string fontFamily)
        {
            var checkFont = new Font(fontFamily, 10);
            if (checkFont.Name.ToLower() == fontFamily.ToLower())
            {
                this.FontFamily = checkFont.Name;
                return true;
            }
            return false;
        }

        public bool TrySetFontColor(string colorName)
        {
            var color = Color.FromName(colorName);
            if (color.Name.ToLower() != colorName.ToLower())
                return false;
            cloudDrawer.ColorProvider = new FixedColorProvider(color);
            return true;
        }

        public void SetFontRandomColor()
        {
            cloudDrawer.ColorProvider = new RandomColorProvider();
        }

        public bool TrySetImageSize(int imageSize)
        {
            if (imageSize > 2000 || imageSize < 100)
                return false;
            cloudDrawer.ChangeImageSize(imageSize);
            return true;
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

        public string GetImageFormat()
        {
            return cloudDrawer.ImageSaver.Format;
        }
    }
}
