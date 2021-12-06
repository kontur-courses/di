using System.Linq;
using TagsCloudContainer.Drawing;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.ImageSaver;
using TagsCloudContainer.Settings;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.WordsConverter;

namespace TagsCloudContainer
{
    public class TagCloudCreator
    {
        private readonly IImageSaver saver;
        private readonly IWordsFilter wordsFilter;
        private readonly IFileReader wordsReader;
        private readonly IDrawer cloudDrawer;
        private readonly IAppSettings appSettings;
        private readonly IWordConverter wordConverter;

        public TagCloudCreator(IImageSaver saver, IWordsFilter wordsFilter, IFileReader wordsReader,
            IDrawer cloudDrawer, IAppSettings appSettings, IWordConverter wordConverter)
        {
            this.saver = saver;
            this.wordsFilter = wordsFilter;
            this.wordsReader = wordsReader;
            this.cloudDrawer = cloudDrawer;
            this.appSettings = appSettings;
            this.wordConverter = wordConverter;
        }

        public void CreateTagCloudImage()
        {
            var words = wordsReader.ReadWordsFromFile(appSettings.InputPath).Select(word => word.ToLower());
            words = wordsFilter.Filter(words);
            var tags = wordConverter.ConvertWords(words);
            var image = cloudDrawer.DrawImage(tags);
            saver.Save(image);
        }
    }
}